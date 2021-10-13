using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Common;
using Domain.Comon;
using WebAPI.DTO;
using Domain.Common;

namespace WebAPI.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class AssetsController : ControllerBase
    {
        readonly SimpleAssetService service;
        public AssetsController(SimpleAssetService _service)
        {
            service = _service;
        }

        [HttpGet]
        public async Task<ActionResult<List<AssetPrice>>> GetAssetSymbolsAsync()
        {
            return Ok(await service.GetAssetSymbolsAsync());
        }

        [HttpGet("{Symbol}/info")]
        public async Task<ActionResult<AssetData>> GetAssetInfoAsync(string Symbol)
        {
            return Ok(await service.GetAssetInfoAsync(Symbol));
        }

        [HttpPost]
        public ActionResult SubUserForUpdates(AddRemoveSubscritionDTO sub)
        {
            return service.SubUserForUpdates(sub.ChatId,sub.Symbol) ? Ok() : BadRequest();

        }

        [HttpDelete("{Symbol}/{ChatId}")]
        public ActionResult UnsubUserFromUpdates(long ChatId, string Symbol)
        {
            return service.UnsubUserFromUpdates(ChatId, Symbol) ? Ok() : NotFound();
        }

        [HttpGet("updates")]
        public async Task<ActionResult<List<GroupedUserSubscription>>> GetAssetUpdatesListAsync()
        {

            return Ok(await service.GetAssetUpdatesListAsync());

        }
        [HttpGet("{Symbol}/exists")]
        public async Task<ActionResult<bool>> AssetExistsAsync(string Symbol)
        {

            return Ok(await service.AssetExistsAsync(Symbol));

        }
        
    }
}
