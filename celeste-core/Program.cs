using System;

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
                .UseStartup<CelesteApi>();
            }).Build();
    }
}
