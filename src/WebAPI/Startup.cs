using Application.Common;
using Application.Common.Interfaces;
using Infrastructure.CryptoAPI;
using Infrastructure.Persistance.MsSqlData;
using Infrastructure.Persistance.MsSqlData.Repository;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System;
using WebAPI.AutoMapper;
using Infrastructure.CryptoAPI.AutoMapper;
using CleanArchitecture.WebUI.Filters;


namespace WebAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSignalR(opt =>
            {
                opt.EnableDetailedErrors = true;
                opt.KeepAliveInterval = TimeSpan.FromMinutes(1);
            });
            services.AddDbContext<NetCampContext>(options =>
              options.UseSqlServer(Configuration.GetConnectionString("Default")),ServiceLifetime.Singleton);
            services.AddSingleton<IUserSubscriptionRepository, UserSubscriptionRepository>();
            services.AddSingleton<IExternalCryptoAPI, LunarCrushAPI>();
            services.AddScoped<SimpleAssetService>();
            services.AddHostedService<LunarCrushHostedService>();
            services.AddControllers(options=>
                options.Filters.Add<AssetExceptionFilter>()
            );
            services.AddAutoMapper(typeof(InfrastructureAutoMapperProfiles).Assembly);
            services.AddAutoMapper(typeof(APIAutoMapperProfiles).Assembly);
            services.AddHttpClient("LunarCrushAPI", c => {
                c.BaseAddress = new Uri(Configuration.GetValue<string>("LunarCrushAPI_URL"));
            });
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "NetCamp_Eleks", Version = "v1" });
            });
            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "NetCamp_Eleks v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapHub<AssetHub>("/AssetHub");
                endpoints.MapControllers();
            });
        }
    }
}
