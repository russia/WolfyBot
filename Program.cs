using System;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WolfyBot.Core.Dispatcher;
using WolfyBot.Core.Packets;

namespace WolfyBot
{
    internal class Program
    {
        public static ulong TotalNetworkReceivedLength = 0;
        public static ulong TotalNetworkSentLength = 0;
        public static bool AreClientsConnectingToGame = true;
        private static void Main(string[] args)
        {
            Console.Title = "Fudjia's WolfyBot";
            Reader.Initialize();
            MessageBuilder.Initialize();
            Client client = new Client("af8b2abe-ae5c-4a51-8f4f-3181481943d5"); //tijem48285@akxpert.com // 8cb85bca-72bb-4722-bc16-1548e2e45eed
            var task = Task.Factory.StartNew(() => client.ConnectToHub());

            ClientsManager.ClientList.Add(client);
            while (true)
            {
                Program.UpdateTitle();
                Thread.Sleep(125);
            }
        }

        public static void WriteColoredLine(string str, ConsoleColor color)
        {
            Console.ResetColor();
            Console.ForegroundColor = color;
            Console.WriteLine(str);
            Console.ResetColor();
        }

        public static void UpdateTitle()
        {
            string formatrcv = TotalNetworkReceivedLength >= 1000000 ? $"{TotalNetworkReceivedLength / 1000000} Mo" : $"{TotalNetworkReceivedLength / 1000} Ko";
            string formatsnd = TotalNetworkSentLength >= 1000000 ? $"{TotalNetworkSentLength / 1000000} Mo" : $"{TotalNetworkSentLength / 1000} Ko";
            Console.Title = $"[{DateTime.Now.ToString("HH:mm:ss")}] Fudjia's WolfyBot | Network data : [Received : {formatrcv} | Sent : {formatsnd}] Current client state : {ClientsManager.ClientList.First().CurrentNetworkState} | Total Elo earned : {ClientsManager.ClientList.Sum(x => x.TotalEloEarned)}";
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