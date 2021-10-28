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
        [InlineData(1, "BTC", false, true)]
        [InlineData(1, "BTC", true, false)]
        public void Subscription_created_when_doesnt_exist(long ChatId,string Symbol,bool subscriptionExists, bool expected)
        {
            repo.Setup(repo => repo.SubscriptionExists(ChatId, Symbol)).Returns(subscriptionExists);
            var sut = new SimpleAssetService(api.Object, repo.Object);

            bool result = sut.SubUserForUpdates(ChatId, Symbol);

            result.Should().Be(expected);
        }
        [Theory]
        [InlineData(1, "BTC", false, false)]
        [InlineData(1, "BTC", true, true)]
        public void Subscription_deleted_when_exists(long ChatId, string Symbol, bool subscriptionExists, bool expected)
        {
            repo.Setup(repo => repo.SubscriptionExists(ChatId, Symbol)).Returns(subscriptionExists);
            var sut = new SimpleAssetService(api.Object, repo.Object);

            bool result = sut.UnsubUserFromUpdates(ChatId, Symbol);

            result.Should().Be(expected);
        }
    }
}
