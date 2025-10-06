using Microsoft.AspNetCore.Mvc;
using StackTags.Server.Models;
using StackTags.Server.Repositories;
using StackTags.Server.Services;

namespace StackTags.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TagsController : ControllerBase
    {
        private readonly ITagRepository _repo;
        private readonly ITagSyncService _sync;

        public TagsController(ITagRepository repo, ITagSyncService sync)
        {
            _repo = repo;
            _sync = sync;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] TagQuery query)
        {
            var result = await _repo.GetPagedAsync(query);
            return Ok(result);
        }

        [HttpPost("sync")]
        public async Task<IActionResult> Sync()
        {
            await _sync.SyncTagsAsync(forceReload: true);
            return NoContent();
        }
    }
}
