using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using Application.Common.Interfaces;
using System.Threading.Tasks;
using System.Net.Http;
using Microsoft.Extensions.Configuration;
using Domain.Common;
using Infrastructure.CryptoAPI.DTO;
using AutoMapper;
using System;
using System.Net;
using Domain.Exceptions;

namespace Infrastructure.CryptoAPI
{
    public class LunarCrushAPI : IExternalCryptoAPI
    {
        readonly HttpClient client;

        readonly string apiKey;

        readonly IMapper mapper;

        public LunarCrushAPI(IConfiguration configuration, IHttpClientFactory clientFactory, IMapper mapper)
        {
            apiKey = configuration.GetSection("LunarCrushAPI_Key").Value;
            client = clientFactory.CreateClient("LunarCrushAPI");
            this.mapper = mapper;
        }

        public async Task<bool> AssetExistsAsync(string Symbol)
        {
            var response = await client.GetAsync($"?data=meta&key={apiKey}&type=price");
            if (!response.IsSuccessStatusCode) throw new ExternaAPIException();
            var result = await response.Content.ReadFromJsonAsync<LunarAssetPriceWrapperDTO>();
            return result.data.Any(a => a.Symbol == Symbol && a.Price.HasValue);
        }
        public async Task<List<AssetPrice>> GetAllAssetsPriceAsync()
        {
            var response = await client.GetAsync($"?data=meta&key={apiKey}&type=price");
            if (!response.IsSuccessStatusCode) throw new ExternaAPIException();
            var result = await response.Content.ReadFromJsonAsync<LunarAssetPriceWrapperDTO>();
            var data = result.data.Where(a=>a.Price.HasValue).ToList();
            return mapper.Map<List<AssetPrice>>(data);
        }
        public async Task<List<AssetPrice>> GetAssetSymbolsAsync(int AssetAmount)
        {
            var response = await client.GetAsync($"?data=meta&key={apiKey}&type=price");
            if (!response.IsSuccessStatusCode) throw new ExternaAPIException();
            var result = await response.Content.ReadFromJsonAsync<LunarAssetPriceWrapperDTO>();
            var data = result.data.OrderByDescending(el => el.Price).Take(AssetAmount).ToList();
            return mapper.Map<List<AssetPrice>>(data);
        }
        public async Task<AssetData> GetAssetInfoAsync(string Symbol)
        {
            var response = await client.GetAsync($"?data=assets&key={apiKey}&symbol={Symbol}");
            if (!response.IsSuccessStatusCode) throw new ExternaAPIException();
            var result = await response.Content.ReadFromJsonAsync<LunarAssetDataWrapperDTO>();
            if (result.data == null || result.data.Count == 0) throw new AssetDoesntExistException(Symbol);
            return mapper.Map<AssetData>(result.data[0]);
        }
    }
}
