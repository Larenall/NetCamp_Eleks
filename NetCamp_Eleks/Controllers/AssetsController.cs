using Application.Common.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
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
        public AssetsController(IDataService context,IExternalCryptoAPI _api)
        {
            db = context;
            api = _api;
        }
        [HttpGet("{Symbol}")]
        public async Task<bool> AssetExists(string Symbol)
        {
            return await api.AssetExists(Symbol);
        }
        [HttpGet]
        public async Task<string> GetCryptoSymbols()
        {
            return await api.GetCryptoSymbols();
        }
        [HttpGet("{Symbol}/info")]
        public async Task<string> GetInfoOnCurrency(string Symbol)
        {
            return await api.GetInfoOnCurrency(Symbol);
        }
    }
}
