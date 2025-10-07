using Microsoft.EntityFrameworkCore;
using StackTags.Server.Data;
using StackTags.Server.Data.Models;
using StackTags.Server.Models;
using StackTags.Server.Repositories;

public class TagRepositoryTests
{
    private TagDbContext GetDbContext()
    {
        var options = new DbContextOptionsBuilder<TagDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDb") 
            .Options;
        return new TagDbContext(options);
    }

    [Fact]
    public async Task HasTagsAsync_ReturnsFalse_WhenNoTags()
    {
        var ctx = GetDbContext();
        var repo = new TagRepository(ctx);

        var result = await repo.HasTagsAsync();

        Assert.False(result);
    }

    [Fact]
    public async Task SaveTagsAsync_AddsTags()
    {
        var ctx = GetDbContext();
        var repo = new TagRepository(ctx);

        var tags = new[]
        {
            new Tag { Name = "csharp", Count = 100, Share = 10.0 },
            new Tag { Name = "dotnet", Count = 200, Share = 20.0 }
        };

        await repo.SaveTagsAsync(tags);

        Assert.Equal(2, ctx.Tags.Count());
    }

    [Fact]
    public async Task GetPagedAsync_ReturnsPagedTags()
    {
        var ctx = GetDbContext();
        var repo = new TagRepository(ctx);

        await repo.SaveTagsAsync(new[]
        {
            new Tag { Name = "a", Count = 1, Share = 1.0 },
            new Tag { Name = "b", Count = 2, Share = 2.0 },
            new Tag { Name = "c", Count = 3, Share = 3.0 }
        });

        var query = new TagQuery { Page = 1, PageSize = 2, SortBy = "name", Order = "asc" };
        var result = await repo.GetPagedAsync(query);

        Assert.Equal(2, result.Count());
        Assert.Equal("a", result.First().Name);
    }
}