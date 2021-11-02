using Application.Common.Interfaces;
using Application.Common;
using Moq;
using System;
using Xunit;
using FluentAssertions;
using Infrastructure.CryptoAPI;
using Microsoft.Extensions.Configuration;
using System.Net.Http;

namespace Application.UnitTests
{
    public class SimpleAssetServiceTests
    {
        readonly Mock<IExternalCryptoAPI> api;
        readonly Mock<IUserSubscriptionRepository> repo;

        public SimpleAssetServiceTests()
        {
            api = new Mock<IExternalCryptoAPI>();
            repo = new Mock<IUserSubscriptionRepository>();
        }

        [Theory]
        [InlineData("1", "BTC","Telegram" ,false, true)]
        [InlineData("1", "BTC", "Telegram",true, false)]
        public void Subscription_created_when_doesnt_exist(string UserId, string Symbol, string Recource, bool subscriptionExists, bool expected)
        {
            repo.Setup(repo => repo.SubscriptionExists(UserId, Symbol,Recource)).Returns(subscriptionExists);
            var sut = new SimpleAssetService(api.Object, repo.Object);

            bool result = sut.SubUserForUpdates(UserId, Symbol, Recource);

            result.Should().Be(expected);
        }
        [Theory]
        [InlineData("1", "BTC", "Telegram", false, false)]
        [InlineData("1", "BTC", "Telegram",true, true)]
        public void Subscription_deleted_when_exists(string UserId, string Symbol, string Recource, bool subscriptionExists, bool expected)
        {
            repo.Setup(repo => repo.SubscriptionExists(UserId, Symbol, Recource)).Returns(subscriptionExists);
            var sut = new SimpleAssetService(api.Object, repo.Object);

            bool result = sut.UnsubUserFromUpdates(UserId, Symbol, Recource);

            result.Should().Be(expected);
        }
    }
}
