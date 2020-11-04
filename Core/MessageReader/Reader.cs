using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using WolfyBot.Core.Packets;

namespace WolfyBot.Core.MessageReader
{
    public class Reader
    {
        public static List<Message> Messages = new List<Message>();
        public static void Initialize()
        {
            //ici je dois faire une liste de mes types, avec cette liste je pourrai chopper le nom et l'objet 

            Assembly assembly = typeof(Message).GetTypeInfo().Assembly; // on get l'assembly ou il y a les messages
            foreach (var type in assembly.GetTypes().ToArray()) // on cherche tout les types qui contiennent des methodes avec l'attribute Message
            {
                Console.WriteLine(type.GetTypeInfo());
            }
            Console.ReadKey();
        }


        public static void MessageReader(string message)
        {
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
            var Type = JObject.Parse(json);
            if(Type["type"] == null) // en attendant on return les apcket qui n'ont pas de type 
                return;
            Program.WriteColoredLine($"[{DateTime.Now.ToString("HH:mm:ss")}] RCV [Name : {packetname} | Type : {Type["type"]}] -> {json}", ConsoleColor.DarkCyan);

            //En gros il faut faire une classe objet pour chaque type et la call a partir du type trouvé
            // une fois qu'on get lobjet rempli, on peut call l'update si il existe en utilisant les datas qu'on a 


        }

    }
}
