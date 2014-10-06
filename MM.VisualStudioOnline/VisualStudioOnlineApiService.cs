using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using MM.VisualStudioOnline.Data;
using MM.VisualStudioOnline.Data.DataAccess;
using MM.VisualStudioOnline.Models;

namespace MM.VisualStudioOnline
{
    public class VisualStudioOnlineApiService
    {
        private const string ADDITIONAL_METADATA_FILENAME = "ADDITIONAL_METADATA.json";

        private readonly IVisualStudioOnlineApiRepository _repository;

        public VisualStudioOnlineApiService(string accountName, string username, string password)
            : this(new VisualStudioOnlineApiRepository(new VisualStudioOnlineConnector(accountName, username, password))
                ) { }

        internal VisualStudioOnlineApiService(IVisualStudioOnlineApiRepository visualStudioOnlineApiRepository)
        {
            _repository = visualStudioOnlineApiRepository;
        }

        public async Task<IEnumerable<Project>> GetProjectsAsync()
        {
            return await _repository.GetProjectsAsync();
        }

        public async Task<IEnumerable<WorkItem>> GetMyWorkItemsAsync(string projectName)
        {
            var queryResult = await _repository.GetMyWorkItemsQueryAsync(projectName);
            return await getWorkItemsAsync(queryResult);
        }

        public async Task<IEnumerable<WorkItem>> GetWorkItemsAsync(string projectName)
        {
            var queryResult = await _repository.GetWorkItemsQueryAsync(projectName);
            return await getWorkItemsAsync(queryResult);
        }

        public async Task<WorkItem> UpdateWorkItemAsync(WorkItem workItem)
        {
            await updateWorkItemAttachments(workItem);

            var updateWorkItem = await mergeUpdatedWorkItemWithOriginal(workItem);

            if(updateWorkItem.Fields.Count > 0)
            {
                var updateResult = await _repository.UpdateWorkItemAsync(updateWorkItem);
                return updateResult;
            }

            workItem.Revision = updateWorkItem.Revision;
            return workItem;
        }

        private async Task updateWorkItemAttachments(WorkItem workItem)
        {
            var originalWorkItems = await _repository.GetWorkItemsAsync(new[]
            {
                workItem.Id
            }, true);

            var originalWorkItem = originalWorkItems.FirstOrDefault();

            if (originalWorkItem == null)
            {
                throw new VisualStudioOnlineApiException("Could not find original work item!");
            }

            // If original work item has additional metadata attachment(s), delete it/them
            var originalAdditionalMetadataAttachmentResource =
                originalWorkItem.Resources.Where(
                    attachment =>
                        attachment.Name.ToLowerInvariant() == ADDITIONAL_METADATA_FILENAME.ToLowerInvariant()).ToList();

            if(originalAdditionalMetadataAttachmentResource.Count > 0)
            {
                var attachedWorkItemRemoval = await _repository.RemoveAttachmentsReferenceToWorkItemAsync(workItem, originalAdditionalMetadataAttachmentResource);
                workItem.Revision = attachedWorkItemRemoval.Revision;
            }

            // Check for additional meta, this will need to be converted to a file and uploaded
            if(workItem.AdditionalMetaData != null)
            {
                var convert = new ExpandoObjectConverter();
                var serialized = JsonConvert.SerializeObject(workItem.AdditionalMetaData, convert);
                var attachment = new Attachment
                {
                    Text = serialized,
                    Area = workItem.AreaPath,
                    Project = workItem.Project,
                    Filename = ADDITIONAL_METADATA_FILENAME,
                };
                workItem.Attachments.Add(attachment);
            }

            if(workItem.Attachments.Count > 0)
            {
                for(var i = 0; i < workItem.Attachments.Count; i++)
                {
                    var attachment = workItem.Attachments[i];
                    if(!string.IsNullOrWhiteSpace(attachment.Text))
                    {
                        workItem.Attachments[i] = await _repository.UploadTextAttachmentAsync(attachment);
                    }
                    else
                    {
                        if(attachment.Data.Length > 0)
                        {
                            workItem.Attachments[i] = await _repository.UploadBinaryAttachmentAsync(attachment);
                        }
                    }
                }

                var revisedAfterAttachmentsWorkItem =
                    await _repository.ReferenceAttachmentsToWorkItemAsync(workItem, workItem.Attachments);
                workItem.Revision = revisedAfterAttachmentsWorkItem.Revision;
            }
        }

        private async Task<IEnumerable<WorkItem>> getWorkItemsAsync(ApiQueryResultCollection queryResult)
        {
            var workItems = await _repository.GetWorkItemsAsync(queryResult.ResultIds, true);
            var workItemsWithAdditionalMetadata = await Task.WhenAll(workItems.Select(async workItem =>
            {
                var additionalMetaData =
                    workItem.Resources.LastOrDefault(
                        resource =>
                            resource.Type.ToLowerInvariant() == "attachment"
                            && resource.Name.ToLowerInvariant() == ADDITIONAL_METADATA_FILENAME.ToLowerInvariant());

                if(additionalMetaData != null)
                {
                    var text = await _repository.DownloadTextAttachmentAsync(additionalMetaData);
                    var jsonString = JsonConvert.DeserializeObject<string>(text);
                    var converter = new ExpandoObjectConverter();
                    dynamic metadata = JsonConvert.DeserializeObject<ExpandoObject>(jsonString, converter);
                    workItem.AdditionalMetaData = metadata;
                }

                var attachments =
                    workItem.Resources.Where(
                        resource =>
                            resource.Type.ToLowerInvariant() == "attachment"
                            && resource.Name.ToLowerInvariant() != ADDITIONAL_METADATA_FILENAME.ToLowerInvariant());

                workItem.Attachments.AddRange(attachments.Select(attachment => new Attachment
                {
                    Url = attachment.Url,
                    Project = workItem.Project,
                    Area = workItem.AreaPath,
                    Id = attachment.Location,
                }));

                return workItem;
            }));

            return workItemsWithAdditionalMetadata;
        }

        private async Task<UpdateWorkItem> mergeUpdatedWorkItemWithOriginal(WorkItem workItem)
        {
            var originalWorkItems = await _repository.GetWorkItemsAsync(new[]
            {
                workItem.Id
            });

            var originalWorkItem = originalWorkItems.FirstOrDefault();

            if(originalWorkItem == null)
            {
                throw new VisualStudioOnlineApiException("Could not find original work item!");
            }

            var updateWorkItem = new UpdateWorkItem
            {
                Id = workItem.Id,
                Revision = workItem.Revision,
            };

            // Find all fields that need to be updated
            var modifiedFields = (from originalField in originalWorkItem.Fields
                let newField = workItem[originalField.ReferenceName]
                where newField != null && newField.Value != originalField.Value
                select new UpdateWorkItemField
                {
                    Metadata = new UpdateWorkItemFieldMetadata
                    {
                        ReferenceName = newField.ReferenceName
                    },
                    Value = newField.Value
                });

            updateWorkItem.Fields.AddRange(modifiedFields);

            // Find new fields
            var newFields =
                workItem.Fields.Where(newField => originalWorkItem[newField.ReferenceName] == null)
                    .Select(newField => new UpdateWorkItemField
                    {
                        Metadata = new UpdateWorkItemFieldMetadata
                        {
                            ReferenceName = newField.ReferenceName
                        },
                        Value = newField.Value
                    });
            updateWorkItem.Fields.AddRange(newFields);

            return updateWorkItem;
        }
    }
}