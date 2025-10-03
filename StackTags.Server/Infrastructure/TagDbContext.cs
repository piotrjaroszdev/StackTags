using Microsoft.EntityFrameworkCore;
using StackTags.Server.Models;
using System.Collections.Generic;

namespace StackTags.Server.Infrastructure
{
    public class TagDbContext : DbContext
    {
        public DbSet<Tag> Tags { get; set; }
        public TagDbContext(DbContextOptions<TagDbContext> options) : base(options) { }
    }

}
