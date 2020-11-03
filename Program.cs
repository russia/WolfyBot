using System;
using System.Threading;

namespace WolfyBot
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Console.Title = "Fudjia's WolfyBot";
            Client client = new Client("8cb85bca-72bb-4722-bc16-1548e2e45eed");
            while (true)
            {
                Thread.Sleep(100);
            }
        }

        public static void WriteColoredLine(string str, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(str);
            Console.ResetColor();
        }
    }
}