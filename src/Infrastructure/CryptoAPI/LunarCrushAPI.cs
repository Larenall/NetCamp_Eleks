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

namespace Infrastructure.CryptoAPI
{
    public class LunarCrushAPI : IExternalCryptoAPI
    {
        readonly HttpClient client;

        readonly string apiKey;

        readonly IMapper mapper;

        public LunarCrushAPI(IConfiguration configuration, IHttpClientFactory _clientFactory, IMapper _mapper)
        {
            apiKey = configuration.GetSection("LunarCrushAPI_Key").Value;
            client = _clientFactory.CreateClient("LunarCrushAPI");
            mapper = _mapper;
        }

        public async Task<bool> AssetExistsAsync(string Symbol)
        {
            var response = await client.GetFromJsonAsync<LunarAssetPriceWrapperDTO>($"?data=meta&key={apiKey}&type=price");
            return response.data.Any(a => a.symbol == Symbol && a.price.HasValue);
        }
        public async Task<List<AssetPrice>> GetAllAssetsPriceAsync()
        {
            var response = await client.GetFromJsonAsync<LunarAssetPriceWrapperDTO>($"?data=meta&key={apiKey}&type=price");
            var data = response.data.Where(a => a.price.HasValue).ToList();
            return mapper.Map<List<AssetPrice>>(data);
        }
        public async Task<List<AssetPrice>> GetAssetSymbolsAsync()
        {
            var response = await client.GetFromJsonAsync<LunarAssetPriceWrapperDTO>($"?data=meta&key={apiKey}&type=price");
            var data = response.data.OrderByDescending(el => el.price).Take(10).ToList();
            return mapper.Map<List<AssetPrice>>(data);
        }
        public async Task<AssetData> GetAssetInfoAsync(string Symbol)
        {
            var response = await client.GetFromJsonAsync<LunarAssetDataWrapperDTO>($"?data=assets&key={apiKey}&symbol={Symbol}");
            return mapper.Map<AssetData>(response.data[0]);
        }
    }
}
