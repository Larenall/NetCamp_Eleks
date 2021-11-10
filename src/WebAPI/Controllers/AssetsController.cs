using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Common;
using WebAPI.DTO;
using AutoMapper;


namespace WebAPI.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class AssetsController : ControllerBase
    {
        readonly SimpleAssetService service;

        readonly IMapper mapper;

        public AssetsController(SimpleAssetService _service, IMapper _mapper)
        {
            service = _service;
            mapper = _mapper;
        }

        [HttpGet("{AssetAmount}")]
        public async Task<ActionResult<List<AssetPriceDTO>>> GetAssetSymbolsAsync(int AssetAmount = 10)
        {
            var symbols = await service.GetAssetSymbolsAsync(AssetAmount);
            var symbolsDTO = mapper.Map<List<AssetPriceDTO>>(symbols);
            return Ok(symbolsDTO);
        }

        [HttpGet("{Symbol}/info")]
        public async Task<ActionResult<AssetDataDTO>> GetAssetInfoAsync(string Symbol)
        {
            var assetData = await service.GetAssetInfoAsync(Symbol);
            var assetDataDTO = mapper.Map<AssetDataDTO>(assetData);
            return Ok(assetDataDTO);
        }
        [HttpPost]
        public async Task<ActionResult> SubUserForUpdates(AddRemoveSubscritionDTO sub)
        {
            if (await service.AssetExistsAsync(sub.Symbol))
                return service.SubUserForUpdates(sub.UserId, sub.Symbol, sub.Recource) ? Ok() : Conflict();
            return BadRequest();
        }

        [HttpDelete("{Recourse}/{Symbol}/{UserId}")]
        public async Task<ActionResult> UnsubUserFromUpdates(string UserId, string Symbol, string Recource)
        {
            if (await service.AssetExistsAsync(Symbol))
                return service.UnsubUserFromUpdates(UserId, Symbol, Recource) ? Ok() : NotFound();
            return BadRequest();
        }

        [HttpGet("{Recource}/updates")]
        public async Task<ActionResult<List<GroupedUserSubscriptionDTO>>> GetAssetUpdatesListAsync(string Recource)
        {
            var subscriptions = await service.GetAssetUpdatesListAsync(Recource);
            var subscriptionsDTO = mapper.Map<List<GroupedUserSubscriptionDTO>>(subscriptions);
            return Ok(subscriptionsDTO);
        }
        [HttpGet("{Symbol}/exists")]
        public async Task<ActionResult<bool>> AssetExistsAsync(string Symbol)
        {
            return Ok(await service.AssetExistsAsync(Symbol));
        }
        
    }
}
