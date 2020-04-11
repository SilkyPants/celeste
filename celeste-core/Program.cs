using System;
using System.IO;
using System.Runtime.InteropServices;

using EliteAPI;
using Somfic.Logging;
using Somfic.Logging.Handlers;

using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;


namespace Celeste
{
    class MainClass
    {
        private static IHost WebHost;

        private static void Main(string[] args)
        {
            Console.CancelKeyPress += (s,e) => {
                    Console.WriteLine("Quitting...");
                    WebHost.StopAsync().Wait();
            };

            WebHost = CreateHost(args);
            WebHost.Run();
        }

        //
        //
        //

        public static IHost CreateHost(string[] args) =>
            Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder
                .UseStartup<CelesteApi>()
                .UseWebRoot("web-ui")
                .UseUrls("https://localhost:5001","http://localhost:5000");
            }).Build();
    }
}
