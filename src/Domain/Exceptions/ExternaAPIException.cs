using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Exceptions
{
    public class ExternaAPIException : Exception
    {
        public ExternaAPIException()
            : base("An error occured during the request to external recource. Recource is not responging and is either malfunctioning or being updated. Try again later")

        {
        }
    }
}
