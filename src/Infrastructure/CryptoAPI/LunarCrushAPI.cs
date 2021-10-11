using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using Application.Common.Interfaces;
using Domain.DTO;
using System.Threading.Tasks;
using System.Net.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Infrastructure.Persistance;
using Infrastructure.Persistance.MsSqlData;

namespace Infrastructure.CryptoAPI
{
    public class LunarCrushAPI : IExternalCryptoAPI
    {
        HttpClient Http = new HttpClient();

        private readonly string apiKey;

        public LunarCrushAPI(IConfiguration configuration)
        {
            apiKey = configuration.GetSection("LunarCrushKey").Value;
        }

        public async Task<bool> AssetExistsAsync(string Symbol)
        {
            AssetDataWrapperDTO response = await Http.GetFromJsonAsync<AssetDataWrapperDTO>($"https://api.lunarcrush.com/v2?data=meta&key={apiKey}&type=price");
            return response.data.Any(a => a.Symbol == Symbol && a.Price.HasValue);
        }
        public async Task<List<AssetPriceDTO>> GetAllAssetsPriceAsync()
        {
            List<AssetPriceDTO> pricesList = new List<AssetPriceDTO>() { };
            AssetDataWrapperDTO response = await Http.GetFromJsonAsync<AssetDataWrapperDTO>($"https://api.lunarcrush.com/v2?data=meta&key={apiKey}&type=price");
            var data = response.data.Where(a => a.Price is not null).ToList();
            data.ForEach(a => pricesList.Add(new AssetPriceDTO(a.Symbol,(double)a.Price)));
            return pricesList;
        }
        public async Task<string> GetAssetSymbolsAsync()
        {
            AssetDataWrapperDTO response = await Http.GetFromJsonAsync<AssetDataWrapperDTO>($"https://api.lunarcrush.com/v2?data=meta&key={apiKey}&type=price");
            var data = response.data.OrderByDescending(el => el.Price).Take(10).ToList();
            string result = "";
            data.ForEach(el => {
                result += $"{el.Symbol} - {el.Price}\n";
            });
            return result;
        }
        public async Task<string> GetAssetInfoAsync(string Symbol)
        {
            var response = await Http.GetFromJsonAsync<AssetDataWrapperDTO>($"https://api.lunarcrush.com/v2?data=assets&key={apiKey}&symbol={Symbol}");
            var result = response.data[0];
            return
                $"\nName - {result.Name}\n" +
                $"% Change in last 24h: {result.Percent_Change_24h}%\n" +
                $"% Change in last 7d: {result.Percent_Change_7d}%\n" +
                $"% Change in last 30d: {result.Percent_Change_30d}%\n" +
                $"Currency rank: {result.alt_rank}\n" +
                $"Currency % change rank: {result.percent_change_24h_rank}";
        }
    }
}
