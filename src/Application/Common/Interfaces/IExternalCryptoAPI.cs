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

        Task<string> GetInfoOnCurrency(string Symbol);
        Task<bool> AssetExists(string Symbol);
        Task<string> GetCryptoSymbols();
        Task<List<UserSubscriptionDTO>> GetCryptoUpdatesList();
    }
}
