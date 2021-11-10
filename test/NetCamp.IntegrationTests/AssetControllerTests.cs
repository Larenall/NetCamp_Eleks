using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAPI;
using AutoFixture;
using WireMock.Server;
using Microsoft.Extensions.DependencyInjection;
using WireMock.RequestBuilders;
using Microsoft.Extensions.Configuration;
using Xunit;
using AutoFixture.Xunit2;
using Infrastructure.CryptoAPI.DTO;
using WireMock.ResponseBuilders;
using System.Net;
using FluentAssertions;
using System.Net.Http.Json;
using WebAPI.DTO;
using AutoMapper;
using Domain.Common;
using System.Net.Http;

namespace NetCamp.IntegrationTests
{
    public class AssetControllerTests : IClassFixture<IntegrationTest<Startup>>
    {
        readonly WireMockServer wireMock;
        readonly HttpClient TestClient;

        public AssetControllerTests(IntegrationTest<Startup> factory)
        {
            TestClient = factory.TestClient;
            wireMock = factory.Services.GetRequiredService<WireMockServer>();
        }
        [Theory,AutoData]
        public async void Recieved_collection_should_be_same_as_sent_collection(LunarAssetPriceWrapperDTO recievedCollection)
        {
            //Arrange
            wireMock.Given(Request.Create().WithParam("data").WithParam("key").WithParam("type").UsingGet())
                .RespondWith(Response.Create().WithBodyAsJson(recievedCollection).WithStatusCode(HttpStatusCode.OK));

            //Act
            var response = await TestClient.GetAsync("api/Assets/10");
            var sentCollection = await response.Content.ReadFromJsonAsync<List<AssetPriceDTO>>();

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            recievedCollection.data.Should().BeEquivalentTo(sentCollection);

        }

        [Theory, AutoData]
        public async void Action_should_return_OK_when_asset_exists(LunarAssetDataWrapperDTO recievedCollection)
        {
            //Arrange
            wireMock.Given(Request.Create().WithParam("data").WithParam("key").WithParam("symbol").UsingGet())
                .RespondWith(Response.Create().WithBodyAsJson(recievedCollection).WithStatusCode(HttpStatusCode.OK));

            //Act
            var response = await TestClient.GetAsync($"api/Assets/{recievedCollection.data[0].symbol}/info");
            var data = await response.Content.ReadFromJsonAsync<AssetDataDTO>();

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            data.Symbol.Should().Be(recievedCollection.data[0].symbol);

        }
    }
}
