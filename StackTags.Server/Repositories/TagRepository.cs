using Microsoft.EntityFrameworkCore;
using StackTags.Server.Data;
using StackTags.Server.Data.Models;
using StackTags.Server.Models;

namespace StackTags.Server.Repositories
{
    public class TagRepository : ITagRepository
    {
        private readonly TagDbContext _ctx;
        public TagRepository(TagDbContext ctx) => _ctx = ctx;

        public async Task<bool> HasTagsAsync() => await _ctx.Tags.AnyAsync();

        public async Task SaveTagsAsync(IEnumerable<Tag> tags)
        {
            _ctx.Tags.RemoveRange(_ctx.Tags);
            await _ctx.Tags.AddRangeAsync(tags);
            await _ctx.SaveChangesAsync();
        }

        public async Task<IEnumerable<Tag>> GetPagedAsync(TagQuery query)
        {
            var tags = _ctx.Tags.AsQueryable();
            tags = query.SortBy switch
            {
                "share" => query.Order == "desc" ? tags.OrderByDescending(t => t.Share) : tags.OrderBy(t => t.Share),
                _ => query.Order == "desc" ? tags.OrderByDescending(t => t.Name) : tags.OrderBy(t => t.Name)
            };
            return await tags.Skip((query.Page - 1) * query.PageSize).Take(query.PageSize).ToListAsync();
        }
    }
}
