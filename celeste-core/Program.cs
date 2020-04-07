using System;
using System.IO;
using System.Threading;
using EliteAPI;
using Somfic.Logging;
using Somfic.Logging.Handlers;

namespace celeste
{
    class MainClass
    {
        private static EliteDangerousAPI EliteAPI;

        private static void Main(string[] args)
        {
            DirectoryInfo journalDirectory = new DirectoryInfo("../../ed-data/");
            EliteAPI = new EliteDangerousAPI(journalDirectory: journalDirectory, skipCatchUp: false);
            
            Logger.AddHandler(new LogFileHandler(Directory.GetCurrentDirectory(), "EliteAPI"));
            Logger.AddHandler(new ConsoleHandler(), Severity.Info, Severity.Warning, Severity.Error, Severity.Special, Severity.Success);

            EliteAPI.OnReset += (s, e) => {
                EliteAPI.Events.CommanderEvent += HandleCommanderInfo;
                EliteAPI.Events.AllEvent += HandleEvent;
                EliteAPI.Events.ShutdownEvent += HandleShutdown;
            };

            EliteAPI.Start();

            while (EliteAPI.IsRunning) {
                
                var keyInfo = Console.ReadKey(intercept: true);

                if (keyInfo.Key == ConsoleKey.Q) {
                    Console.WriteLine("Quitting...");
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

            if (ev is Newtonsoft.Json.Linq.JObject)
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
    }
}
