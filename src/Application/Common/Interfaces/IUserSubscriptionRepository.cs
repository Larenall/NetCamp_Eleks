using Domain.Common;
using Domain.Entities;
using System.Collections.Generic;

namespace Application.Common.Interfaces
{
    public interface IUserSubscriptionRepository
    {
        void AddSubscription(string ChatId, string Symbol, string Recource);
        void AddSubscription(UserSubscription entityToAdd);
        void DeleteSubscription(string ChatId, string Symbol, string Recource);
        List<UserSubscription> GetSubscriptions();
        List<GroupedUserSubscription> GetGroupedSubscriptions(string Recource);
        void Save();
        bool SubscriptionExists(string ChatId, string Symbol, string Recource);
    }
}
