namespace MM.VisualStudioOnline.Data
{
    internal interface IVisualStudioOnlineApiRepository : IBuildDefinitionsRepository, IProjectsRepository,
        IWorkItemsRepository, IAttachmentsRepository { }
}