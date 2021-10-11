using Domain.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Interfaces
{
    public interface IExternalCryptoAPI
    {

        Task<string> GetAssetInfoAsync(string Symbol);
        Task<bool> AssetExistsAsync(string Symbol);
        Task<string> GetAssetSymbolsAsync();
        Task<List<UserSubscriptionDTO>> GetAssetUpdatesListAsync();
    }
}
