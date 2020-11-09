using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WolfyBot.Core.Dispatcher;
using WolfyBot.Core.Game;
using WolfyBot.Core.Game.Types;
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
            //List<PlayerRole> PlayerList = new List<PlayerRole>();
            //List<PlayerRole> AlliesIds = new List<PlayerRole>();
            //var player1 = new PlayerRole("23e3cb21-b041-4949-9732-41d10c95d80b");
            //var player2 = new PlayerRole("7997c42e-ed15-409c-8b35-a72d31802ab6");
            //var player3 = new PlayerRole("37edd268-afef-4517-82f0-3566f105b5ce");
            //var player4 = new PlayerRole("3ccc34ea-ae78-4217-a694-f8ddbc0d0781");
            //var player5 = new PlayerRole("a168073c-7d55-47ca-86dd-05797e1e3084");
            //var player6 = new PlayerRole("b7af9922-352f-47cc-b14e-d8a4ce3b44bf");
            //PlayerList.Add(player1);
            //PlayerList.Add(player2);
            //PlayerList.Add(player3);
            //PlayerList.Add(player4);
            //PlayerList.Add(player5);
            //PlayerList.Add(player6);

            //AlliesIds.Add(player1);
            //AlliesIds.Add(player2);


            //var list = IAHelper.RemoveBfromA(PlayerList, AlliesIds);

            Console.Title = "Fudjia's WolfyBot";
            Reader.Initialize();
            MessageBuilder.Initialize();
            Client client = new Client("836839f6-e0eb-49db-8bb2-029bc00adc0e", "4d71b716-7bab-4182-9c47-763d6422190b"); //tijem48285@akxpert.com // 8cb85bca-72bb-4722-bc16-1548e2e45eed
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