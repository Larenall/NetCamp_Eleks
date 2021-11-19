using Application.Common.Interfaces;
using Domain.Common;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Infrastructure.Persistance.MsSqlData.Repository
{
    public class UserSubscriptionRepository : IUserSubscriptionRepository
    {
        internal NetCampContext context;


        public UserSubscriptionRepository(NetCampContext context)
        {
            this.context = context;
        }

        public List<UserSubscription> GetSubscriptions()
        {
            return context.UserSubscriptions.ToList();
        }
        public void AddSubscription(string UserId, string Symbol, string Recource)
        {
            context.UserSubscriptions.Add(new UserSubscription(UserId, Symbol, Recource));
            context.SaveChanges();
        }

        public void AddSubscription(UserSubscription entityToAdd)
        {
            context.UserSubscriptions.Add(entityToAdd);
            context.SaveChanges();
        }
        
        public void DeleteSubscription(string UserId, string Symbol, string Recource)
        {
            UserSubscription customer = context.UserSubscriptions.FirstOrDefault(s => s.Resource == Recource && s.UserId == UserId && s.Symbol == Symbol);
            context.UserSubscriptions.Remove(customer);
            context.SaveChanges();
        }
        public void DeleteSubscription(UserSubscription entityToDelete)
        {
            UserSubscription subscription = context.UserSubscriptions.FirstOrDefault(s => s == entityToDelete);
            context.UserSubscriptions.Remove(subscription);
            context.SaveChanges();
        }
        public bool SubscriptionExists(string UserId, string Symbol, string Recource)
        {
            return context.UserSubscriptions.Any(s => s.Resource == Recource && s.UserId == UserId && s.Symbol == Symbol);
        }
        public List<GroupedUserSubscription> GetGroupedSubscriptions()
        {
            return context.UserSubscriptions.ToList()
                .GroupBy(el => el.Symbol, el => (el.UserId,el.Resource), (Symbol, subDataTuples) => new GroupedUserSubscription(Symbol, subDataTuples.ToList()))
                .ToList();
        }
        public List<string> GetAllResources()
        {
            return context.UserSubscriptions.Select(el => el.Resource).Distinct().ToList();
        }
    }
}
