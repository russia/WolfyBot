using System;
using System.Threading;
using WolfyBot.Core.MessageReader;

namespace WolfyBot
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Console.Title = "Fudjia's WolfyBot";
            //Reader.Initialize();
            Client client = new Client("8cb85bca-72bb-4722-bc16-1548e2e45eed");
            while (true)
            {
                Thread.Sleep(2000);
            }
        }

        public static void WriteColoredLine(string str, ConsoleColor color)
        {
            Console.ResetColor();
            Console.ForegroundColor = color;
            Console.WriteLine(str);
            Console.ResetColor();
        }
    }
}