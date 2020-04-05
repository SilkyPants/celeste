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
            DirectoryInfo journalDirectory = new DirectoryInfo("../ed-data/");
            EliteAPI = new EliteDangerousAPI(journalDirectory: journalDirectory, skipCatchUp: false);
            EliteAPI.Logger.AddHandler(new ConsoleHandler());

            EliteAPI.Start();

            Thread.Sleep(-1);
            Console.WriteLine("Hello World!");
        }
    }
}
