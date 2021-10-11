﻿using Application.Common.Interfaces;
using Domain.DTO;
using Domain.Entities;
using Infrastructure.Persistance.MsSqlData;
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
        public void AddSubscription(long ChatId, string Symbol)
        {
            context.UserSubscriptions.Add(new UserSubscription(ChatId, Symbol));
        }

        public void AddSubscription(UserSubscription entityToAdd)
        {
            context.UserSubscriptions.Add(entityToAdd);
        }
        
        public void DeleteSubscription(long ChatId, string Symbol)
        {
            UserSubscription customer = context.UserSubscriptions.FirstOrDefault(s => s.ChatId == ChatId && s.Symbol == Symbol);
            context.UserSubscriptions.Remove(customer);
        }
        public void DeleteSubscription(UserSubscription entityToDelete)
        {
            UserSubscription subscription = context.UserSubscriptions.FirstOrDefault(s => s == entityToDelete);
            context.UserSubscriptions.Remove(subscription);
        }
        public bool SubscriptionExists(long ChatId, string Symbol)
        {
            return context.UserSubscriptions.Any(s => s.ChatId == ChatId && s.Symbol == Symbol);
        }
        public List<UserSubscriptionDTO> GroupBySymbols()
        {
            return context.UserSubscriptions.ToList().GroupBy(el => el.Symbol, el => el.ChatId, (Symbol, ChatId) => new UserSubscriptionDTO(Symbol, ChatId.ToList())).ToList();
        }
        public void Save()
        {
            context.SaveChanges();
        }
    }
}
