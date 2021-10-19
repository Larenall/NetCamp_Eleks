using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using Application.Common.Interfaces;
using System.Threading.Tasks;
using System.Net.Http;
using Microsoft.Extensions.Configuration;
using Domain.Common;

namespace Infrastructure.CryptoAPI
{
    public class LunarCrushAPI : IExternalCryptoAPI
    {
        readonly HttpClient client;

        readonly string apiKey;

        public LunarCrushAPI(IConfiguration configuration, IHttpClientFactory _clientFactory)
        {
            apiKey = configuration.GetSection("LunarCrushAPI_Key").Value;
            client = _clientFactory.CreateClient("LunarCrushAPI");
        }

        public async Task<bool> AssetExistsAsync(string Symbol)
        {
            AssetDataWrapper response = await client.GetFromJsonAsync<AssetDataWrapper>($"?data=meta&key={apiKey}&type=price");
            return response.data.Any(a => a.Symbol == Symbol && a.Price.HasValue);
        }
        public async Task<List<AssetPrice>> GetAllAssetsPriceAsync()
        {
            List<AssetPrice> pricesList = new List<AssetPrice>() { };
            AssetDataWrapper response = await client.GetFromJsonAsync<AssetDataWrapper>($"?data=meta&key={apiKey}&type=price");
            var data = response.data.Where(a => a.Price is not null).ToList();
            data.ForEach(a => pricesList.Add(new AssetPrice(a.Symbol,(double)a.Price)));
            return pricesList;
        }
        public async Task<List<AssetPrice>> GetAssetSymbolsAsync()
        {
            AssetDataWrapper response = await client.GetFromJsonAsync<AssetDataWrapper>($"?data=meta&key={apiKey}&type=price");
            var data = response.data.OrderByDescending(el => el.Price).Take(10).ToList();
            var result = new List<AssetPrice>();
            data.ForEach(d => result.Add(new AssetPrice(d.Symbol,(double)d.Price)));
            return result;
        }
        public async Task<AssetData> GetAssetInfoAsync(string Symbol)
        {
            var response = await client.GetFromJsonAsync<AssetDataWrapper>($"?data=assets&key={apiKey}&symbol={Symbol}");
            return response.data[0];
        }
    }
}
