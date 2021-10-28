using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Common;
using WebAPI.DTO;
using AutoMapper;
using System;
using Domain.Exceptions;

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

        [HttpGet("{AssetAmount}")]
        public async Task<ActionResult<List<AssetPriceDTO>>> GetAssetSymbolsAsync(int AssetAmount = 10)
        {
            try
            {
                var symbols = await service.GetAssetSymbolsAsync(AssetAmount);
                var symbolsDTO = mapper.Map<List<AssetPriceDTO>>(symbols);
                return Ok(symbolsDTO);
            }
            catch (AssetDoesntExistException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{Symbol}/info")]
        public async Task<ActionResult<AssetDataDTO>> GetAssetInfoAsync(string Symbol)
        {
            try
            {
                var assetData = await service.GetAssetInfoAsync(Symbol);
                var assetDataDTO = mapper.Map<AssetDataDTO>(assetData);
                return Ok(assetDataDTO);
            }
            catch (AssetDoesntExistException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult> SubUserForUpdates(AddRemoveSubscritionDTO sub)
        {
            if (await service.AssetExistsAsync(sub.Symbol))
                return service.SubUserForUpdates(sub.ChatId,sub.Symbol) ? Ok() : Conflict();
            return BadRequest();
        }

        [HttpDelete("{Symbol}/{ChatId}")]
        public async Task<ActionResult> UnsubUserFromUpdates(long ChatId, string Symbol)
        {
            if(await service.AssetExistsAsync(Symbol))
                return service.UnsubUserFromUpdates(ChatId, Symbol) ? Ok() : NotFound();
            return BadRequest();
        }

        [HttpGet("updates")]
        public async Task<ActionResult<List<GroupedUserSubscriptionDTO>>> GetAssetUpdatesListAsync()
        {
            try
            {
                var subscriptions = await service.GetAssetUpdatesListAsync();
                var subscriptionsDTO = mapper.Map<List<GroupedUserSubscriptionDTO>>(subscriptions);
                return Ok(subscriptionsDTO);
            }
            catch (AssetDoesntExistException ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("{Symbol}/exists")]
        public async Task<ActionResult<bool>> AssetExistsAsync(string Symbol)
        {

            return Ok(await service.AssetExistsAsync(Symbol));

        }
        
    }
}
