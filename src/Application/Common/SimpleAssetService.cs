using Application.Common.Interfaces;
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
