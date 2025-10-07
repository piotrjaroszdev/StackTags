using StackTags.Server.Data.Models;
using StackTags.Server.Models;

namespace StackTags.Server.Repositories
{
    public interface ITagRepository
    {
        Task<bool> HasTagsAsync();
        Task SaveTagsAsync(IEnumerable<Tag> tags);
        Task<IEnumerable<Tag>> GetPagedAsync(TagQuery query);
    }
}
