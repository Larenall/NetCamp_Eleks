using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Common;
using WebAPI.DTO;
using Domain.Common;
using AutoMapper;

namespace WebAPI.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class AssetsController : ControllerBase
    {
        readonly SimpleAssetService service;

        readonly IMapper mapper;

        public AssetsController(SimpleAssetService _service,IMapper _mapper)
        {
            service = _service;
            mapper = _mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<AssetPriceDTO>>> GetAssetSymbolsAsync()
        {
            var symbols = await service.GetAssetSymbolsAsync();
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
        public async Task<ActionResult<List<GroupedUserSubscriptionDTO>>> GetAssetUpdatesListAsync()
        {
            var subscriptions = await service.GetAssetUpdatesListAsync();
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
