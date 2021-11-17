﻿using Application.Common.Interfaces;
using Domain.Common;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace Application.Common
{
    public class SimpleAssetService
    {
        readonly IUserSubscriptionRepository repository;
        readonly IExternalCryptoAPI api;
        private List<AssetPrice> currentPrice = new List<AssetPrice>();

        public SimpleAssetService(IExternalCryptoAPI api, IUserSubscriptionRepository repository)
        {
            this.repository = repository;
            this.api = api;
        }
        public bool SubUserForUpdates(string UserId, string Symbol,string Recource)
        {
            if (!repository.SubscriptionExists(UserId, Symbol, Recource))
            {
                repository.AddSubscription(UserId, Symbol, Recource);
                return true;
            }
            return false;

        }
        public bool UnsubUserFromUpdates(string UserId, string Symbol, string Recource)
        {
            if (repository.SubscriptionExists(UserId, Symbol, Recource))
            {
                repository.DeleteSubscription(UserId, Symbol, Recource);
                return true;
            }
            return false;
        }
        public async Task<List<GroupedUserSubscription>> GetAssetUpdatesListAsync(string Recource)
        {
            List<AssetPrice> changedCrypto = new List<AssetPrice>() { };

            List<GroupedUserSubscription> assets = repository.GetGroupedSubscriptionsForRecource(Recource);

            List<AssetPrice> newPrice = await api.GetAllAssetsPriceAsync();

            newPrice.ForEach(n =>
            {
                if (!currentPrice.Any(c => c.Symbol == n.Symbol))
                {
                    currentPrice.Add(n);
                }
                else if (currentPrice.FirstOrDefault(c => c.Symbol == n.Symbol).Price != n.Price)
                {
                    changedCrypto.Add(n);
                    currentPrice.FirstOrDefault(c => c.Symbol == n.Symbol).Price = n.Price;
                }

            });

            List<AssetPrice> subbedAndChangedCrypto = changedCrypto
                .Where(ch => ch.Symbol == assets.FirstOrDefault(a => a.Symbol == ch.Symbol)?.Symbol)
                .ToList();

            if (subbedAndChangedCrypto.Count() != 0)
            {
                subbedAndChangedCrypto.ForEach(ch =>
                {
                    assets.FirstOrDefault(a => a.Symbol == ch.Symbol).Price = ch.Price;
                });
                assets = assets.Where(a => a.Price != 0.0).ToList();
                return assets;

            }
            return new List<GroupedUserSubscription>() { };

        }
        public async Task<bool> AssetExistsAsync(string Symbol)
        {
            return await api.AssetExistsAsync(Symbol);
        }
        public async Task<List<AssetPrice>> GetAssetSymbolsAsync(int AssetAmount)
        {
            return await api.GetAssetSymbolsAsync(AssetAmount);
        }
        public async Task<AssetData> GetAssetInfoAsync(string Symbol)
        {
            return await api.GetAssetInfoAsync(Symbol);
        }
    }
}
