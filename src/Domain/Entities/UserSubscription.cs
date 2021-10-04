using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace Domain.Entities
{
    public partial class UserSubscription
    {
        public int Id { get; set; }
        public long ChatId { get; set; }
        [Required]
        [MaxLength(10)]
        public string Symbol { get; set; }

        public UserSubscription()
        {

        }
        public UserSubscription(long ChatId,string Symbol)
        {
            this.ChatId = ChatId;
            this.Symbol = Symbol;
        }
    }
}
