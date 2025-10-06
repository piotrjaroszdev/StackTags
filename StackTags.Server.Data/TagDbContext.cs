using Microsoft.EntityFrameworkCore;
using StackTags.Server.Data.Models;

namespace StackTags.Server.Data
{
    public class TagDbContext : DbContext
    {
        public DbSet<Tag> Tags { get; set; }
        public TagDbContext(DbContextOptions<TagDbContext> options) : base(options) { }
    }

}
