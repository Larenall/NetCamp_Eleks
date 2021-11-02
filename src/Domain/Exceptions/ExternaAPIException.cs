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
            : base("Something went wrong while making a request to external API.")
        {
        }
    }
}
