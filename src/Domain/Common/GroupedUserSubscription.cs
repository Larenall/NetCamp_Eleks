using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Comon
{
    public class GroupedUserSubscription
    {
        public string Symbol { get; set; }
        public double Price { get; set; }
        public List<long> ChatIdList { get; set; }
        public GroupedUserSubscription(string Symbol, double Price, List<long> ChatIdList)
        {
            this.Symbol = Symbol;
            this.Price = Price;
            this.ChatIdList = ChatIdList;
        }
        public GroupedUserSubscription(string Symbol,List<long> ChatIdList)
        {
            this.Symbol = Symbol;
            this.ChatIdList = ChatIdList;
        }
    }
    
}
