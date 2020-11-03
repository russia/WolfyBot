using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WolfyBot.Core.MessageReader
{
    public class Reader
    {
        public static void MessageReader(string message)
        {
            //removing trash 
            while (!message.StartsWith("[")) //TODO improve this part
            {
                message = message.Remove(0, 1);
            }
            while (!message.EndsWith("]"))
            {
                message = message.Remove(message.Length - 1, 1);
            }
            string packetname = message.Substring(2, message.IndexOf(",{") - 3);
            string json = message.Replace($"[\"{packetname}\",", "");
            json = json.Replace("]", "");
            Program.WriteColoredLine($"[{DateTime.Now.ToString("HH:mm:ss")}] RCV [{packetname}] -> {json}", ConsoleColor.DarkCyan);
        }

    }
}
