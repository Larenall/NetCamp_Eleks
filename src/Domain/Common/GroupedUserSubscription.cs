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
        public List<string> UserIdList { get; set; }
        public GroupedUserSubscription(string Symbol, double Price, List<string> UserIdList)
        {
            this.Symbol = Symbol;
            this.Price = Price;
            this.UserIdList = UserIdList;
        }
        public GroupedUserSubscription(string Symbol, List<string> UserIdList)
        {
            this.Symbol = Symbol;
            this.UserIdList = UserIdList;
        }
    }
    
}
