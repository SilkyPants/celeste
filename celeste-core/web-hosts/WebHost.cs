using System;
using Celeste.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;


namespace Celeste
{
    public class CelesteWebHost
    {
        private IHost WebHost;

        public void RunHost(Action<HostBuilderContext, IServiceCollection> services)
        {
            Console.CancelKeyPress += (s, e) => {
                Console.WriteLine("Quitting...");
                WebHost.Services.GetRequiredService<EliteApiService>().Stop();
                WebHost.StopAsync().Wait();
            };

            WebHost = Host.CreateDefaultBuilder()
            .ConfigureServices(services)
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder
                .UseStartup<CelesteApi>()
                .UseWebRoot("web-ui")
                .UseUrls("https://*:5001", "http://*:5000");
            }).Build();

            WebHost.Run();
        }
    }
}
