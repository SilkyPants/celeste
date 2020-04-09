using System;
using System.IO;
using System.Runtime.InteropServices;

using EliteAPI;
using Somfic.Logging;
using Somfic.Logging.Handlers;

using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;


namespace celeste
{
    /*
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
    */

    
    class MainClass
    {
        private static EliteDangerousAPI EliteAPI;

        private static void Main(string[] args)
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows)) {
                EliteAPI = new EliteDangerousAPI();
            }
            else {
                DirectoryInfo journalDirectory = new DirectoryInfo("../../ed-data/");
                EliteAPI = new EliteDangerousAPI(journalDirectory);
            }

            Logger.AddHandler(new LogFileHandler(Directory.GetCurrentDirectory(), "EliteAPI"));
            Logger.AddHandler(new ConsoleHandler(), Severity.Info, Severity.Verbose, Severity.Warning, Severity.Error, Severity.Success);

            EliteAPI.OnReset += (s, e) => {
                EliteAPI.Events.CommanderEvent += HandleCommanderInfo;
                EliteAPI.Events.AllEvent += HandleEvent;
            };

            EliteAPI.Start();

            var host = Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder
                    .UseStartup<Startup>()
                    .UseWebRoot("webapp");
                }).Build();

            host.RunAsync();

            Console.WriteLine("Hit q to stop");
            while (EliteAPI.IsRunning) {
                
                var keyInfo = Console.ReadKey(intercept: true);

                if (keyInfo.Key == ConsoleKey.Q) {
                    Console.WriteLine("Quitting...");
                    host.StopAsync().Wait();
                    EliteAPI.Stop();
                }
            }
        }

        private static void HandleShutdown(object sender, EliteAPI.Events.ShutdownInfo e)
        {
            Console.WriteLine($"Shutdown event detected..");
        }

        private static void HandleCommanderInfo(object sender, EliteAPI.Events.Startup.CommanderInfo e)
        {
            Console.WriteLine($"Commander detected is: {e.Name}");
        }

        private static void HandleEvent(object sender, dynamic ev)
        {
            // Upload to all Inara accounts
            // Upload to all EDSM accounts
            // Upload to EDDN?

            Console.WriteLine("=-=-=-=-=-=-=-=-=-=-=-=-=-");
            if (ev is EliteAPI.Events.EventBase)
            {
                var eventBase = ev as EliteAPI.Events.EventBase;
                Console.WriteLine($"Event Handled: {eventBase}");
            }
            else if (ev is Newtonsoft.Json.Linq.JObject)
            {
                var json = ev as Newtonsoft.Json.Linq.JObject;
                Console.WriteLine($"Raw event: {json}");
            }
            else
            {
                var t = ev.GetType();
                Console.WriteLine($"Unknown: {t.ToString()}");
            }
        }

        //
        //
        //

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
