using Application.Common.Interfaces;
using Domain.DTO;
using Domain.Entities;
using Infrastructure.Persistance.MsSqlData;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Common;


namespace NetCamp_Eleks.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class AssetsController : ControllerBase
    {
        readonly NamePlaceholderService service;
        public AssetsController(NamePlaceholderService _service)
        {
            service = _service;
        }

        [HttpGet]
        public async Task<ActionResult<string>> GetAssetSymbolsAsync()
        {
            return Ok(await service.GetAssetSymbolsAsync());
        }

        [HttpGet("{Symbol}/info")]
        public async Task<ActionResult<string>> GetAssetInfoAsync(string Symbol)
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
        public async Task<ActionResult<List<UserSubscriptionDTO>>> GetAssetUpdatesListAsync()
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
