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
            EliteAPI = new EliteDangerousAPI(journalDirectory: journalDirectory, skipCatchUp: true);
            
            Logger.AddHandler(new LogFileHandler(Directory.GetCurrentDirectory(), "EliteAPI"));
            Logger.AddHandler(new ConsoleHandler(), Severity.Info, Severity.Warning, Severity.Error, Severity.Special, Severity.Success);

            EliteAPI.Start();
            
            while(EliteAPI.IsRunning) {
                var keyInfo = Console.ReadKey(intercept: true);

                if (keyInfo.Key == ConsoleKey.Q) {
                    Console.WriteLine("Quitting...");
                    EliteAPI.Stop();
                }
            }
        }
    }
}
