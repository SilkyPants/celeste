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
        private static IHost WebHost;

        private static void Main(string[] args)
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows)) {
                EliteAPI = new EliteDangerousAPI();
            }
            else {
                DirectoryInfo journalDirectory = new DirectoryInfo("../../ed-data/");
                EliteAPI = new EliteDangerousAPI(journalDirectory);
            }

            Console.CancelKeyPress += (s,e) => {
                    Console.WriteLine("Quitting...");
                    WebHost.StopAsync().Wait();
                    //EliteAPI.Stop();
            };

            Logger.AddHandler(new LogFileHandler(Directory.GetCurrentDirectory(), "EliteAPI"));
            Logger.AddHandler(new ConsoleHandler(), Severity.Info, Severity.Verbose, Severity.Warning, Severity.Error, Severity.Success);

            EliteAPI.OnReset += (s, e) => {
                EliteAPI.Events.CommanderEvent += HandleCommanderInfo;
                EliteAPI.Events.AllEvent += HandleEvent;
            };

            //EliteAPI.Start();

            WebHost = CreateHost(args);
            WebHost.Run();
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

        public static IHost CreateHost(string[] args) =>
                Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder
                    .UseStartup<Startup>()
                    .UseWebRoot("web-ui")
                    .UseUrls("https://localhost:5001","http://localhost:5000");
                }).Build();
    }
}
