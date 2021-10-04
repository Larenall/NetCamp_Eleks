using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using UI;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace NetCamp_Eleks
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
            Console.ReadLine();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
