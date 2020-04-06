using System;
using System.IO;
using System.Threading;
using EliteAPI;
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
            Somfic.Logging.Logger.AddHandler(new ConsoleHandler());

            string path = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
                    Console.WriteLine($"User folder is: {path}");

            EliteAPI.Start();
            
            while(EliteAPI.IsRunning) {
                var keyInfo = Console.ReadKey();

                if (keyInfo.Key == ConsoleKey.Q) {
                    Console.WriteLine("Quitting...");
                    EliteAPI.Stop();
                }
            }
        }
    }
}
