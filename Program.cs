using System;
using System.IO;
using System.Linq;
using System.Threading;
using WolfyBot.Core.Dispatcher;
using WolfyBot.Core.Packets;

namespace WolfyBot
{
    internal class Program
    {
        //tijem48285@akxpert.com // 8cb85bca-72bb-4722-bc16-1548e2e45eed
        public static ulong TotalNetworkReceivedLength = 0;
        public static ulong TotalNetworkSentLength = 0;
        public static bool AreClientsConnectingToGame = true;

        private static void Main(string[] args)
        {
            Console.Title = "Fudjia's WolfyBot";
            Reader.Initialize();
            MessageBuilder.Initialize();
            ClientsManager.Initialize();
            while (true)
            {
                ClientsManager.CheckForClientWaiters();
                Program.UpdateTitle();
                Thread.Sleep(125);
            }
        }

        public static void WriteColoredLine(string str, ConsoleColor color, Client client = null)
        {
            Console.ResetColor();
            Console.ForegroundColor = color;
            if (client != null)
                Console.WriteLine($"[{client.ClientInstanceid}] " + str);
            else
                Console.WriteLine(str);
            Console.ResetColor();
        }

        public static void UpdateTitle()
        {
            string formatrcv = TotalNetworkReceivedLength >= 1000000 ? $"{TotalNetworkReceivedLength / 1000000} Mo" : $"{TotalNetworkReceivedLength / 1000} Ko";
            string formatsnd = TotalNetworkSentLength >= 1000000 ? $"{TotalNetworkSentLength / 1000000} Mo" : $"{TotalNetworkSentLength / 1000} Ko";
            Console.Title = $"[{DateTime.Now.ToString("HH:mm:ss")}] Fudjia's WolfyBot | Games : [{ClientsManager.ClientList.Count(x => x.CurrentNetworkState == Core.Enums.NetworkEnum.LOGGED_GAME)}] | Network data : [Received : {formatrcv} | Sent : {formatsnd}] | Total Elo earned : {ClientsManager.ClientList.Sum(x => x.TotalEloEarned)}";
        }

        private static object jlock = new object();

        public static void AddUnknowMsg(string msg)
        {
            lock (jlock)
            {
                if (!Directory.Exists("./unknowedMsg/"))
                    Directory.CreateDirectory("./unknowedMsg/");
                string path = $"./unknowedMsg/unknowedMsg.txt";
                if (!File.Exists(path))
                    using (StreamWriter sw = File.CreateText(path))
                        sw.WriteLine($"{msg}");
                else
                    using (StreamWriter sw = File.AppendText(path))
                        sw.WriteLine($"{msg}");
            }
            UpdateTitle();
        }
    }
}