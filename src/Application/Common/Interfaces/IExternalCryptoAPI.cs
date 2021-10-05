using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Interfaces
{
    public interface IExternalCryptoAPI
    {

        void StartCheckingForChanges();
        string SubUserForUpdates(long ChatId, string Symbol);
        Task<string> GetInfoOnCurrency(string Symbol);
        string UnsubUserFromUpdates(long ChatId, string Symbol);
        Task<bool> AssetExists(string Symbol);

        event Action<long, string> SendMessage;
        Task<string> GetCryptoSymbols();
    }
}
