using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTO
{
    public class UserSubscriptionDTO
    {
        public string Symbol { get; set; }
        public double Price { get; set; }
        public List<long> ChatIdList { get; set; }
        public UserSubscriptionDTO(string Symbol, double Price, List<long> ChatIdList)
        {
            this.Symbol = Symbol;
            this.Price = Price;
            this.ChatIdList = ChatIdList;
        }
        public UserSubscriptionDTO(string Symbol,List<long> ChatIdList)
        {
            this.Symbol = Symbol;
            this.ChatIdList = ChatIdList;
        }
    }
    
}
