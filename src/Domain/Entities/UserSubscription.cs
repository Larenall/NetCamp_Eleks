using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace Domain.Entities
{
    public partial class UserSubscription
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string Symbol { get; set; }

        public string Resource { get; set; }

        public UserSubscription()
        {

        }
        public UserSubscription(string UserId, string Symbol, string Resource)
        {
            this.Resource = Resource;
            this.UserId = UserId;
            this.Symbol = Symbol;
        }
    }
}
