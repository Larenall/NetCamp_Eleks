using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Exceptions
{
    public class AssetDoesntExistException : Exception
    {
        public AssetDoesntExistException(string Symbol)
            : base($"Asset \"{Symbol}\" doensn`t exist.")
        {
        }
    }
}
