using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using Application.Common.Interfaces;
using Domain.DTO;
using System.Threading.Tasks;
using System.Net.Http;
using System;

namespace Infrastructure
{
    public class LunarCrushAPI : IExternalCryptoAPI
    {
        HttpClient Http = new HttpClient();

        readonly IDataService db;

        List<AssetPriceDTO> currentPrice = new List<AssetPriceDTO>();

        const string LunarCrushApiKey = "no1pctizzjxzzkbilo9xe";

        public LunarCrushAPI(IDataService context)
        {
            db = context;
        }

        public async Task<List<UserSubscriptionDTO>> GetAssetUpdatesListAsync()
        {
            List<AssetPriceDTO> newPrice = new List<AssetPriceDTO>() { };
            List<AssetPriceDTO> changedCrypto = new List<AssetPriceDTO>() { };
            List<UserSubscriptionDTO> assets = db.UserSubscriptions.ToList().GroupBy(el => el.Symbol, el => el.ChatId, (Symbol, ChatId) => new UserSubscriptionDTO(Symbol, ChatId.ToList())).ToList();

            AssetDataWrapperDTO response = await Http.GetFromJsonAsync<AssetDataWrapperDTO>($"https://api.lunarcrush.com/v2?data=meta&key={LunarCrushApiKey}&type=price");
            response.data.ForEach(el =>
            {
                if (el.Price is not null)
                {
                    newPrice.Add(new AssetPriceDTO(el.Symbol, (double)el.Price));
                }
            });

            newPrice.ForEach(n =>
            {
                if(!currentPrice.Any(c => c.Symbol == n.Symbol))
                {
                    currentPrice.Add(n);
                }else if (currentPrice.FirstOrDefault(c => c.Symbol == n.Symbol).Price != n.Price)
                {
                    changedCrypto.Add(n);
                    currentPrice.FirstOrDefault(c => c.Symbol == n.Symbol).Price = n.Price;
                }
                
            });

            var subbedAndChangedCrypto = changedCrypto.Where(ch => ch.Symbol == assets.FirstOrDefault(a => a.Symbol == ch.Symbol)?.Symbol).ToList();

            if (subbedAndChangedCrypto.Count() != 0)
            {
                subbedAndChangedCrypto.ForEach(ch =>
                {
                    assets.FirstOrDefault(a => a.Symbol == ch.Symbol).Price = ch.Price;
                });
                assets = assets.Where(a => a.Price != 0.0).ToList();
                return assets;
                
            }
            return new List<UserSubscriptionDTO>() { };

        }
        public async Task<bool> AssetExistsAsync(string Symbol)
        {
            AssetDataWrapperDTO response = await Http.GetFromJsonAsync<AssetDataWrapperDTO>($"https://api.lunarcrush.com/v2?data=meta&key={LunarCrushApiKey}&type=price");
            return response.data.Any(a => a.Symbol == Symbol && a.Price.HasValue);
        }

        public async Task<string> GetAssetSymbolsAsync()
        {
            AssetDataWrapperDTO response = await Http.GetFromJsonAsync<AssetDataWrapperDTO>($"https://api.lunarcrush.com/v2?data=meta&key={LunarCrushApiKey}&type=price");
            var data = response.data.OrderByDescending(el => el.Price).Take(10).ToList();
            string result = "";
            data.ForEach(el => {
                result += $"{el.Symbol} - {el.Price}\n";
            });
            return result;
        }
        public async Task<string> GetAssetInfoAsync(string Symbol)
        {
            if(!(await AssetExistsAsync(Symbol)))
            {
                return "Couldn`t find any info on given asset :(";
            }
            var response = await Http.GetFromJsonAsync<AssetDataWrapperDTO>($"https://api.lunarcrush.com/v2?data=assets&key={LunarCrushApiKey}&symbol={Symbol}");
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
