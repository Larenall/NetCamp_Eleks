using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTO
{
    public class CryptoPriceDTO
    {
        public string Symbol { get; set; }
        public double Price { get; set; }
        public CryptoPriceDTO(string Symbol, double Price)
        {
            this.Symbol = Symbol;
            this.Price = Price;

        }
    }
}
