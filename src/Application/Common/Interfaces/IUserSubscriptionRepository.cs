using Domain.DTO;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Application.Common.Interfaces
{
    public interface IUserSubscriptionRepository
    {
        void AddSubscription(long ChatId, string Symbol);
        void AddSubscription(UserSubscription entityToAdd);
        void DeleteSubscription(long ChatId, string Symbol);
        List<UserSubscription> GetSubscriptions();
        List<UserSubscriptionDTO> GroupBySymbols();
        void Save();
        bool SubscriptionExists(long ChatId, string Symbol);
    }
}
