using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Common 
{ 
    public class AssetPrice
    {
        public string Symbol { get; set; }
        public double Price { get; set; }
        public AssetPrice(string Symbol, double Price)
        {
            this.Symbol = Symbol;
            this.Price = Price;

        }
    }
}
