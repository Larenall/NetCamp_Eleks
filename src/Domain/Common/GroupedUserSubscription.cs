using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Common
{
    public class GroupedUserSubscription
    {
        public string Symbol { get; set; }
        public double Price { get; set; }
        public List<(string UserId, string Recource)> SubData { get; set; }
        public GroupedUserSubscription(string Symbol, double Price, List<(string, string)> SubData)
        {
            this.Symbol = Symbol;
            this.Price = Price;
            this.SubData = SubData;
        }
        public GroupedUserSubscription(string Symbol, List<(string, string)> SubData)
        {
            this.Symbol = Symbol;
            this.SubData = SubData;
        }
    }
    
}
