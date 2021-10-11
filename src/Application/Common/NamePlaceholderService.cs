using Application.Common.Interfaces;
using Domain.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace Application.Common
{
    public class NamePlaceholderService
    {
        readonly IUserSubscriptionRepository repository;
        readonly IExternalCryptoAPI api;
        private List<AssetPriceDTO> currentPrice = new List<AssetPriceDTO>();

        public NamePlaceholderService(IExternalCryptoAPI api, IUserSubscriptionRepository repository)
        {
            this.repository = repository;
            this.api = api;
        }
        public bool SubUserForUpdates(long ChatId, string Symbol)
        {
            if (!repository.SubscriptionExists(ChatId,Symbol))
            {
                repository.AddSubscription(ChatId, Symbol);
                repository.Save();
                return true;
            }
            return false;

        }
        public bool UnsubUserFromUpdates(long ChatId, string Symbol)
        {
            if (repository.SubscriptionExists(ChatId, Symbol))
            {
                repository.DeleteSubscription(ChatId, Symbol);
                repository.Save();
                return true;
            }
            return false;
        }
        public async Task<List<UserSubscriptionDTO>> GetAssetUpdatesListAsync()
        {
            List<AssetPriceDTO> changedCrypto = new List<AssetPriceDTO>() { };

            List<UserSubscriptionDTO> assets = repository.GroupBySymbols();

            List<AssetPriceDTO> newPrice = await api.GetAllAssetsPriceAsync();

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

            List<AssetPriceDTO> subbedAndChangedCrypto = changedCrypto.Where(ch => ch.Symbol == assets.FirstOrDefault(a => a.Symbol == ch.Symbol)?.Symbol).ToList();

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
            return await api.AssetExistsAsync(Symbol);
        }
        public async Task<string> GetAssetSymbolsAsync()
        {
            return await api.GetAssetSymbolsAsync();
        }
        public async Task<string> GetAssetInfoAsync(string Symbol)
        {
            return await api.GetAssetInfoAsync(Symbol);
        }
    }
}
