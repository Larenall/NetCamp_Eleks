using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAPI.DTO
{
    public class GroupedUserSubscriptionDTO
    {
        public string Symbol { get; set; }
        public double Price { get; set; }
        public List<long> ChatIdList { get; set; }
        public GroupedUserSubscriptionDTO(string Symbol, double Price, List<long> ChatIdList)
        {
            this.Symbol = Symbol;
            this.Price = Price;
            this.ChatIdList = ChatIdList;
        }
        public GroupedUserSubscriptionDTO(string Symbol,List<long> ChatIdList)
        {
            this.Symbol = Symbol;
            this.ChatIdList = ChatIdList;
        }
    }
    
}
