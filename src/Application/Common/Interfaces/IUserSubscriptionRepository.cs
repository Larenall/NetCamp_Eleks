using Domain.Common;
using Domain.Entities;
using System.Collections.Generic;

namespace Application.Common.Interfaces
{
    public interface IUserSubscriptionRepository
    {
        void AddSubscription(string UserId, string Symbol, string Recource);
        void AddSubscription(UserSubscription entityToAdd);
        void DeleteSubscription(string UserId, string Symbol, string Recource);
        List<UserSubscription> GetSubscriptions();
        List<GroupedUserSubscription> GetGroupedSubscriptions();
        bool SubscriptionExists(string UserId, string Symbol, string Recource);
        List<string> GetAllResources();
    }
}
