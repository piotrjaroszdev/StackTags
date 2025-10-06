using StackTags.Server.Models;
using StackTags.Server.Repositories;

namespace StackTags.Server.Services
{
    public class TagSyncService : ITagSyncService
    {
        private readonly ITagRepository _repo;
        private readonly IHttpClientFactory _httpFactory;
        private readonly IConfiguration _config;
        public TagSyncService(ITagRepository repo, IHttpClientFactory httpFactory, IConfiguration config)
        {
            _repo = repo;
            _httpFactory = httpFactory;
            _config = config;
        }

        public async Task SyncTagsAsync(bool forceReload = false)
        {
            if (!forceReload && await _repo.HasTagsAsync()) return;

            var tags = new List<Tag>();
            int page = 1;
            int pageSize = _config.GetValue<int>("StackExchange:PageSize");
            int maxTags = _config.GetValue<int>("StackExchange:MaxTags");
            string baseUrl = _config.GetValue<string>("StackExchange:BaseUrl");
            string site = _config.GetValue<string>("StackExchange:Site");
            string userAgent = _config.GetValue<string>("StackExchange:UserAgent");
            while (tags.Count < 1000)
            {
                var client = _httpFactory.CreateClient();
                client.DefaultRequestHeaders.UserAgent.ParseAdd("StackTagsApp/1.0");

                var url = $"{baseUrl}/tags?site={site}&pagesize={pageSize}&page={page}";
                var response = await client.GetFromJsonAsync<ApiResponse>(url);
                tags.AddRange(response.Items.Select(t => new Tag
                {
                    Name = t.Name,
                    Count = t.Count
                }));
                page++;
            }

            int total = tags.Sum(t => t.Count);
            foreach (var tag in tags)
                tag.Share = Math.Round((double)tag.Count / total * 100, 2);

            await _repo.SaveTagsAsync(tags);
        }
    }

}
