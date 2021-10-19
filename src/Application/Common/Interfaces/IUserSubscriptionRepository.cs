using Domain.Common;
using Domain.Entities;
using System.Collections.Generic;

namespace Application.Common.Interfaces
{
    public interface IUserSubscriptionRepository
    {
        void AddSubscription(long ChatId, string Symbol);
        void AddSubscription(Domain.Entities.UserSubscription entityToAdd);
        void DeleteSubscription(long ChatId, string Symbol);
        List<UserSubscription> GetSubscriptions();
        List<GroupedUserSubscription> GroupBySymbols();
        void Save();
        bool SubscriptionExists(long ChatId, string Symbol);
    }
}
