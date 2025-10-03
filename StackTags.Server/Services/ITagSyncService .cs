namespace StackTags.Server.Services
{
    public interface ITagSyncService
    {
        Task SyncTagsAsync(bool forceReload = false);
    }
}
