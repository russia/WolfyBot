using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WolfyBot.Helper;

namespace WolfyBot
{
    public class ClientsManager
    {
        public static List<Client> ClientList = new List<Client>();
        public static List<Task> TaskList = new List<Task>();

        public static void Initialize()
        {
            for (int i = 0; i < Constants.MaxParallelGamesNumber; i++)
            {
                Client client = new Client("836839f6-e0eb-49db-8bb2-029bc00adc0e", "4d71b716-7bab-4182-9c47-763d6422190b");
                ClientsManager.ClientList.Add(client);
            }
            Program.WriteColoredLine("[Clients Manager] Initalized !", ConsoleColor.Cyan);
        }

        public static void CheckForClientWaiters()
        {
            if (!ClientList.Any(x => x.isWaitingToConnect)) //si aucun client a besoin de se connecter on return
                return;
            if (ClientList.Any(x => x.isCurrentlyConnecting)) //si un client est deja en train de se connecter, on return
                return;

            var client = ClientList.First(x => x.isWaitingToConnect);
            Thread.Sleep(1000);
            var task = Task.Factory.StartNew(() => client.ConnectToHub());
            TaskList.Add(task);
           // TaskList.Remove(TaskList.First(x => x.IsCompleted));
        }
    }
}