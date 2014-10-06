using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using PortableRest;
using MM.VisualStudioOnline.Data.DataAccess;
using MM.VisualStudioOnline.Models;

namespace MM.VisualStudioOnline.Data
{
    internal class VisualStudioOnlineApiRepository : IVisualStudioOnlineApiRepository
    {
        private readonly IVisualStudioOnlineConnector _visualStudioOnlineConnector;

        public VisualStudioOnlineApiRepository(IVisualStudioOnlineConnector visualStudioOnlineConnector)
        {
            _visualStudioOnlineConnector = visualStudioOnlineConnector;
        }

        #region IVisualStudioOnlineApiRepository Members

        public async Task<IEnumerable<BuildDefinition>> GetBuildDefinitionsAsync()
        {
            var buildDefinitions =
                await _visualStudioOnlineConnector.GetApiCollectionRequestAsync<BuildDefinition>("build/definitions");

            return buildDefinitions;
        }

        public async Task<IEnumerable<Project>> GetProjectsAsync()
        {
            var projects = await _visualStudioOnlineConnector.GetApiCollectionRequestAsync<Project>("projects");

            return projects;
        }

        #endregion

        private async Task<ApiQueryResultCollection> getWorkItemsAsync(string projectName, string query)
        {
            var workItems = await _visualStudioOnlineConnector.GetApiQueryRequestAsync("wit/queryresults", request =>
            {
                request.Method = HttpMethod.Post;
                request.ContentType = ContentTypes.Json;
                request.AddQueryString("@project", projectName);

                var content = new
                {
                    wiql = query
                };
                request.AddParameter(content);
            });

            return workItems;
        }

        #region Implementation of IWorkItemsRepository

        public async Task<ApiQueryResultCollection> GetWorkItemsQueryAsync(string projectName)
        {
            return
                await
                    getWorkItemsAsync(projectName,
                        "Select [System.WorkItemType],[System.Title],[System.State],[Microsoft.VSTS.Scheduling.Effort],[System.IterationPath] FROM WorkItemLinks WHERE Source.[System.WorkItemType] IN GROUP 'Microsoft.RequirementCategory' AND Target.[System.WorkItemType] IN GROUP 'Microsoft.RequirementCategory' AND Target.[System.State] IN ('New','Approved','Committed') AND [System.Links.LinkType] = 'System.LinkTypes.Hierarchy-Forward' ORDER BY [Microsoft.VSTS.Common.BacklogPriority] ASC,[System.Id] ASC MODE (Recursive, ReturnMatchingChildren)");
        }

        public async Task<ApiQueryResultCollection> GetMyWorkItemsQueryAsync(string projectName)
        {
            return
                await
                    getWorkItemsAsync(projectName,
                        "Select * From WorkItems WHERE [System.TeamProject] = @project AND [System.WorkItemType] = 'Task' AND [State] <> 'Done' AND [System.AssignedTo] = @Me order by [Microsoft.VSTS.Common.Priority] asc, [System.CreatedDate] desc");
        }

        public async Task<IEnumerable<WorkItem>> GetWorkItemsAsync(IEnumerable<int> workItemIds,
            bool includeAttachments = false)
        {
            var workItems =
                await _visualStudioOnlineConnector.GetApiCollectionRequestAsync<WorkItem>("wit/workitems", request =>
                {
                    request.AddQueryString("ids", string.Join(",", workItemIds));
                    if(includeAttachments)
                    {
                        request.AddQueryString("$expand", "all");
                    }
                });

            return workItems;
        }

        public async Task<WorkItem> UpdateWorkItemAsync(UpdateWorkItem workItem)
        {
            var item =
                await
                    _visualStudioOnlineConnector.GetApiRequestAsync<WorkItem>("wit/workitems/" + workItem.Id, request =>
                    {
                        // HttpMethod doesn't support "patch", so we're implementing this hack
                        request.Method = HttpMethod.Post;
                        request.AddHeader("X-HTTP-Method-Override", "PATCH");
                        request.ContentType = ContentTypes.Json;

                        request.AddParameter(workItem);
                    });

            return item;
        }

        public async Task<WorkItem> ReferenceAttachmentsToWorkItemAsync(WorkItem workItem,
            IEnumerable<Attachment> attachments)
        {
            var item =
                await
                    _visualStudioOnlineConnector.GetApiRequestAsync<WorkItem>("wit/workitems/" + workItem.Id, request =>
                    {
                        // HttpMethod doesn't support "patch", so we're implementing this hack
                        request.Method = HttpMethod.Post;
                        request.AddHeader("X-HTTP-Method-Override", "PATCH");
                        request.ContentType = ContentTypes.Json;

                        var itemRequest = new
                        {
                            id = workItem.Id,
                            rev = workItem.Revision,
                            resourceLinks = attachments.Select(attachment => new
                            {
                                type = "attachment",
                                name = attachment.Filename,
                                location = attachment.Id.ToString(),
                                comment = "Attaching file via the API"
                            })
                        };

                        request.AddParameter(itemRequest);
                    });

            return item;
        }

        public async Task<WorkItem> RemoveAttachmentsReferenceToWorkItemAsync(WorkItem workItem, IEnumerable<WorkItemResource> workItemResources)
        {
            var item =
                await
                    _visualStudioOnlineConnector.GetApiRequestAsync<WorkItem>("wit/workitems/" + workItem.Id, request =>
                    {
                        // HttpMethod doesn't support "patch", so we're implementing this hack
                        request.Method = HttpMethod.Post;
                        request.AddHeader("X-HTTP-Method-Override", "PATCH");
                        request.ContentType = ContentTypes.Json;

                        var itemRequest = new
                        {
                            id = workItem.Id,
                            rev = workItem.Revision,
                            resourceLinks = workItemResources.Select(workItemResource => new
                            {
                                updateType = "delete",
                                resourceId = workItemResource.Id.ToString()
                            })
                        };

                        request.AddParameter(itemRequest);
                    });

            return item;
        }

        #endregion

        #region Implementation of IAttachmentsRepository

        public async Task<Attachment> UploadTextAttachmentAsync(Attachment attachment)
        {
            var url = string.Format(@"wit/attachments?project={0}&area={1}&filename={2}", attachment.Project,
                attachment.Area, attachment.Filename);

            var returnedAttachment = await _visualStudioOnlineConnector.GetApiRequestAsync<Attachment>(url, request =>
            {
                // HttpMethod doesn't support "patch", so we're implementing this hack
                request.Method = HttpMethod.Post;
                request.ContentType = ContentTypes.Json;

                request.AddParameter(attachment.Text);
            });

            attachment.Length = returnedAttachment.Length;
            attachment.Id = returnedAttachment.Id;
            attachment.Url = returnedAttachment.Url;
            attachment.Source = returnedAttachment.Source;

            return attachment;
        }

        public async Task<Attachment> UploadBinaryAttachmentAsync(Attachment attachment)
        {
            throw new NotImplementedException();
        }

        public async Task<string> DownloadTextAttachmentAsync(WorkItemResource attachmentResource)
        {
            var returnedText =
                await _visualStudioOnlineConnector.GetRawRequestAsync("wit/attachments/" + attachmentResource.Location);

            return returnedText;
        }

        public Task<byte[]> DownloadBinaryAttachmentAsync(Attachment attachment)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}