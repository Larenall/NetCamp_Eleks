using Application.Common;
using Application.Common.Interfaces;
using AutoMapper;
using Domain.Common;
using Infrastructure.CryptoAPI.DTO;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Hosting;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Timers;

namespace Infrastructure.CryptoAPI
{
    public class LunarCrushHostedService : IHostedService
    {
        private List<AssetPrice> currentPrice = new();
        readonly IUserSubscriptionRepository repository;
        readonly IExternalCryptoAPI api;
        readonly Timer timer = new(10000);
        readonly IHubContext<AssetHub> hub;
        readonly IMapper mapper;


        public LunarCrushHostedService(IExternalCryptoAPI api, IUserSubscriptionRepository repository, IHubContext<AssetHub> hub, IMapper mapper)
        {
            this.mapper = mapper;
            this.repository = repository;
            this.api = api;
            this.hub = hub;
            timer.Elapsed += SendChanges;
        }

        private async void SendChanges(object sender, ElapsedEventArgs e)
        {
            var updates = await GetAssetUpdatesListAsync();
            if (updates.Count != 0)
            {
                var resources = repository.GetAllResources();
                resources.ForEach(res =>
                {
                    updates.ForEach(el => el.SubData.Where(d => d.Recource == res));
                    var subscriptionsDTO = mapper.Map<List<GroupedUserSubscriptionDTO>>(updates);
                    hub.Clients.All.SendAsync(res, subscriptionsDTO);
                });
            }
        }

        public async Task<List<GroupedUserSubscription>> GetAssetUpdatesListAsync()
        {
            List<AssetPrice> changedCrypto = new() { };

            List<GroupedUserSubscription> assets = repository.GetGroupedSubscriptions();

            List<AssetPrice> newPrice = await api.GetAllAssetsPriceAsync();

            newPrice.ForEach(n =>
            {
                if (!currentPrice.Any(c => c.Symbol == n.Symbol))
                {
                    currentPrice.Add(n);
                }
                else if (currentPrice.FirstOrDefault(c => c.Symbol == n.Symbol).Price != n.Price)
                {
                    changedCrypto.Add(n);
                    currentPrice.FirstOrDefault(c => c.Symbol == n.Symbol).Price = n.Price;
                }

            });

            List<AssetPrice> subbedAndChangedCrypto = changedCrypto
                .Where(ch => ch.Symbol == assets.FirstOrDefault(a => a.Symbol == ch.Symbol)?.Symbol)
                .ToList();

            if (subbedAndChangedCrypto.Count != 0)
            {
                subbedAndChangedCrypto.ForEach(ch =>
                {
                    assets.FirstOrDefault(a => a.Symbol == ch.Symbol).Price = ch.Price;
                });
                return assets.Where(a => a.Price != 0.0).ToList();

            }
            return new List<GroupedUserSubscription>() { };

        }

        public Task StartAsync(System.Threading.CancellationToken cancellationToken)
        {
            timer.Start();
            SendChanges(null, null);
            return Task.CompletedTask;
        }

        public Task StopAsync(System.Threading.CancellationToken cancellationToken)
        {
            timer.Stop();
            return Task.CompletedTask;
        }
    }
}
