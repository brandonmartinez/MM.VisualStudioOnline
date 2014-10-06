using System.Collections.Generic;
using System.Threading.Tasks;
using MM.VisualStudioOnline.Models;

namespace MM.VisualStudioOnline.Data
{
    internal interface IAttachmentsRepository
    {
        Task<Attachment> UploadTextAttachmentAsync(Attachment attachment);

        Task<Attachment> UploadBinaryAttachmentAsync(Attachment attachment);

        Task<string> DownloadTextAttachmentAsync(WorkItemResource attachmentResource);

        Task<byte[]> DownloadBinaryAttachmentAsync(Attachment attachment);

        Task<WorkItem> RemoveAttachmentsReferenceToWorkItemAsync(WorkItem workItem, IEnumerable<WorkItemResource> workItemResources);
    }
}