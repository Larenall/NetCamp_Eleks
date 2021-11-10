using Microsoft.AspNetCore.Mvc.Testing;
using System.Net.Http;
using WebAPI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Infrastructure.Persistance.MsSqlData;
using System.Net.Http.Json;
using System.Threading.Tasks;
using WireMock;
using Microsoft.AspNetCore.Hosting;
using WireMock.Server;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using AutoMapper;
using System;

namespace NetCamp.IntegrationTests
{
    public class IntegrationTest<TStartup> : WebApplicationFactory<TStartup> where TStartup : class
    {
        public readonly HttpClient TestClient;
        readonly WireMockServer wireMockServer;
        public IntegrationTest()
        {
            wireMockServer = WireMockServer.Start();
            var appFactory = new WebApplicationFactory<Startup>()
                .WithWebHostBuilder(builder =>
                {
                    builder.ConfigureServices(services =>
                    {
                        services.RemoveAll(typeof(NetCampContext));
                        services.AddDbContext<NetCampContext>(options =>
                            options.UseInMemoryDatabase("TestDb"),ServiceLifetime.Singleton);
                        services.RemoveAll(typeof(HttpClient));
                        services.AddHttpClient("LunarCrushAPI", c => {
                            c.BaseAddress = new Uri(wireMockServer.Urls[0]);
                        });
                    });
                });
            
            TestClient = appFactory.CreateClient();

        }
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            
            builder.ConfigureServices(services => services.AddSingleton(wireMockServer));

        }
    }
}
