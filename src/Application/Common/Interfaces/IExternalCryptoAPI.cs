
using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Interfaces
{
    public interface IExternalCryptoAPI
    {

        Task<AssetData> GetAssetInfoAsync(string Symbol);
        Task<bool> AssetExistsAsync(string Symbol);
        Task<List<AssetPrice>> GetAssetSymbolsAsync();
        Task<List<AssetPrice>> GetAllAssetsPriceAsync();
    }
}
