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
        string SignUserForUpdates(long ChatId, string Symbol);
        string CancelUpdates(long ChatId, string Symbol);

        event Action<long, string> SendMessage;

    }
}
