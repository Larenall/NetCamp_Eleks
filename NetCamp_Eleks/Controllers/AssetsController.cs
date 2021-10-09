using Application.Common.Interfaces;
using Domain.DTO;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace NetCamp_Eleks.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class AssetsController : ControllerBase
    {
        readonly IDataService db;
        readonly IExternalCryptoAPI api;
        public AssetsController(IDataService context, IExternalCryptoAPI _api)
        {
            db = context;
            api = _api;
        }

        [HttpGet]
        public async Task<ActionResult<string>> GetCryptoSymbols()
        {
            var value = await api.GetCryptoSymbols();
            return Ok(value);
        }

        [HttpGet("{Symbol}/info")]
        public async Task<ActionResult<string>> GetInfoOnCurrency(string Symbol)
        {
            var value = await api.GetInfoOnCurrency(Symbol);
            return Ok(value);
        }

        [HttpPost]
        public ActionResult SubUserForUpdates(AddRemoveSubscritionDTO sub)
        {
            if (!db.UserSubscriptions.Any(e => e.ChatId == sub.ChatId && e.Symbol == sub.Symbol))
            {
                db.UserSubscriptions.Add(new UserSubscription(sub.ChatId, sub.Symbol));
                db.SaveChanges();
                return Ok();
            }
            return BadRequest();

        }

        [HttpDelete("{Symbol}/{ChatId}")]
        public ActionResult UnsubUserFromUpdates(long ChatId, string Symbol)
        {
            if (db.UserSubscriptions.Any(e => e.ChatId == ChatId && e.Symbol == Symbol))
            {
                var subscrToRemove = db.UserSubscriptions.FirstOrDefault(el => el.ChatId == ChatId && el.Symbol == Symbol);
                db.UserSubscriptions.Remove(subscrToRemove);
                db.SaveChanges();
                return Ok();
            }
            return NotFound();
        }

        [HttpGet("updates")]
        public async Task<ActionResult<List<UserSubscriptionDTO>>> GetCryptoUpdatesList()
        {
            var value = await api.GetCryptoUpdatesList();
            return Ok(value);

        }
        [HttpGet("{Symbol}/exists")]
        public async Task<ActionResult<bool>> AssetExists(string Symbol)
        {
            var value = await api.AssetExists(Symbol);
            return Ok(value);

        }
        
    }
}
