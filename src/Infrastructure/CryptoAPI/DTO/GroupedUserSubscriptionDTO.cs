using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.CryptoAPI.DTO
{
    public class GroupedUserSubscriptionDTO
    {
        public GroupedUserSubscriptionDTO()
        {

        }
        public string Symbol { get; set; }
        public double Price { get; set; }
        public List<string> UserIdList { get; set; }
        public GroupedUserSubscriptionDTO(string Symbol, double Price, List<string> UserIdList)
        {
            this.Symbol = Symbol;
            this.Price = Price;
            this.UserIdList = UserIdList;
        }
        public GroupedUserSubscriptionDTO(string Symbol, List<string> UserIdList)
        {
            this.Symbol = Symbol;
            this.UserIdList = UserIdList;
        }
    }
    
}
