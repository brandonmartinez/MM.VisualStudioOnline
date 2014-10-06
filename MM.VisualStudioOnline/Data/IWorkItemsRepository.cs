using System.Collections.Generic;
using System.Threading.Tasks;
using MM.VisualStudioOnline.Models;

namespace MM.VisualStudioOnline.Data
{
    internal interface IWorkItemsRepository
    {
        Task<ApiQueryResultCollection> GetWorkItemsQueryAsync(string projectName);

        Task<ApiQueryResultCollection> GetMyWorkItemsQueryAsync(string projectName);

        Task<IEnumerable<WorkItem>> GetWorkItemsAsync(IEnumerable<int> workItemIds, bool includeAttachments = false);

        Task<WorkItem> UpdateWorkItemAsync(UpdateWorkItem workItem);

        Task<WorkItem> ReferenceAttachmentsToWorkItemAsync(WorkItem workItem, IEnumerable<Attachment> attachments);
    }
}