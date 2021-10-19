using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAPI.DTO
{ 
    public class AssetPriceDTO
    {
        public string Symbol { get; set; }
        public double Price { get; set; }
        public AssetPriceDTO(string Symbol, double Price)
        {
            this.Symbol = Symbol;
            this.Price = Price;

        }
    }
}
