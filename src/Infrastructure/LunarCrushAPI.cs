using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Timers;
using Application.Common.Interfaces;
using Domain.DTO;
using Domain.Common;
using Infrastructure.Persistance;
using Domain.Entities;

namespace Infrastructure
{
    public class LunarCrushAPI : IExternalCryptoAPI
    {
        HttpClient Http = new HttpClient();

        Timer timer = new Timer(30000);

        readonly IDbContext db;

        List<CryptoPriceDTO> currentPrice = new List<CryptoPriceDTO>() { };

        const string LunarCrushApiKey = "no1pctizzjxzzkbilo9xe";

        public event Action<long, string> SendMessage;


        public LunarCrushAPI(IDbContext context)
        {
            db = context;
        }

        public void StartCheckingForChanges()
        {
            SetCurerntPrice();
            UpdateUsersOnPrice(null, null);
            timer.Elapsed += UpdateUsersOnPrice;
            timer.Start();

        }
        async void UpdateUsersOnPrice(object sender, EventArgs e)
        {
            List<CryptoPriceDTO> newPrice = new List<CryptoPriceDTO>() { };
            List<CryptoPriceDTO> changedCrypto = new List<CryptoPriceDTO>() { };
            List<UserSubscriptionDTO> assets = db.UserSubscriptions.ToList().GroupBy(el => el.Symbol, el => el.ChatId, (Symbol, ChatId) => new UserSubscriptionDTO(Symbol, ChatId.ToList())).ToList();
            if (assets.All(a=>a.ChatIdList.Count() == 0))
            {
                return;
            }

            AssetDataWrapper response = await Http.GetFromJsonAsync<AssetDataWrapper>($"https://api.lunarcrush.com/v2?data=meta&key={LunarCrushApiKey}&type=price");
            response.data.ForEach(el =>
            {
                if (el.Price is not null)
                {
                    newPrice.Add(new CryptoPriceDTO(el.Symbol, (double)el.Price));
                }
            });

            newPrice.ForEach(n =>
            {
                if (currentPrice.FirstOrDefault(c => c.Symbol == n.Symbol).Price != n.Price)
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

                assets.ForEach(a =>
                {
                    a.ChatIdList.ForEach(chatId =>
                    {
                        SendMessage(chatId, $"{a.Symbol} is worth  - {a.Price} USD");
                    });
                });
            }

        }

        async void SetCurerntPrice()
        {
            AssetDataWrapper response = await Http.GetFromJsonAsync<AssetDataWrapper>($"https://api.lunarcrush.com/v2?data=meta&key={LunarCrushApiKey}&type=price");
            response.data.ForEach(el => {
                if (el.Price is not null)
                {
                    currentPrice.Add(new CryptoPriceDTO(el.Symbol, (double)el.Price));
                }
            });
        }

        public string SignUserForUpdates(long ChatId, string Symbol="BTC")
        {
            if (!db.UserSubscriptions.Any(e => e.ChatId == ChatId && e.Symbol == Symbol))
            {
                db.UserSubscriptions.Add(new UserSubscription(ChatId, Symbol));
                db.SaveChanges();
                return "I`ll keep you updated";
            }
            return "You alredy signed for this currency";
            
        }

        public string CancelUpdates(long ChatId, string Symbol)
        {
            if (db.UserSubscriptions.Any(e => e.ChatId == ChatId && e.Symbol == Symbol))
            {
                var subscrToRemove = db.UserSubscriptions.FirstOrDefault(el => el.ChatId == ChatId && el.Symbol == "BTC");
                db.UserSubscriptions.Remove(subscrToRemove);
                db.SaveChanges();
                return "Ok, no more updates";
            }
            return "You weren`t subbed anyway :/";
        }
    }
}
