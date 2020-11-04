using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using WolfyBot.Core.Packets;

namespace WolfyBot.Core.Dispatcher
{
    public class Reader
    {
        private static List<PacketData> Methods { get; set; } = new List<PacketData>();

        public static void Initialize()
        {
            Assembly assembly = typeof(Message).GetTypeInfo().Assembly; // on get l'assembly ou il y a les messages

            foreach (var type in assembly.GetTypes().SelectMany(x => x.GetMethods()).Where(m => m.GetCustomAttributes(typeof(MessageTypeAttribute), false).Length > 0).ToArray()) // on cherche tout les types qui contiennent des methodes avec l'attribute Message
            {
                var Attributes = type.GetCustomAttributes(typeof(MessageTypeAttribute), true).Select(x => (MessageTypeAttribute)x); // on get les attributes de la methode
                foreach (var attr in Attributes)
                {
                    Type stringType = Type.GetType(type.DeclaringType.FullName); // on get le Type de la methode depuis sa classe

                    var instance = Activator.CreateInstance(stringType, null); // on cree une instance de la methode

                    Methods.Add(new PacketData(instance, attr.Value, attr.runSynchronously, type));// on save les donnees de la methode
                }
            }
        }

        public static void MessageReader(Client client, string message)
        {
            Console.WriteLine(message);
            message = WolfyBot.Core.Helper.Extensions.CleanPacket("[", "]", message);
            string packetname = message.Substring(2, message.IndexOf(",{") - 3);
            string json = message.Replace($"[\"{packetname}\",", "");
            json = json.Replace("]", "");

            var Type = JObject.Parse(json);

            if (Type["type"] == null)
            {
                Program.WriteColoredLine($"[{DateTime.Now.ToString("HH:mm:ss")}] RCV [Name : {packetname} | Type : NOTYPE] -> {json}", ConsoleColor.DarkCyan);
                foreach (var method in Methods.Where(x => x.Key.ToLower() == packetname.ToLower())) // on cherche si il y a un handler avec le meme nom de message
                {
                    method.Methode.Invoke(method.Instance, new object[] { client, MessageBuilder.GetMessage(packetname.ToLower(), Type) });
                }
                return;
            }

            if (!Methods.Where(x => x.Key == Type["type"].ToString()).Any())
            {
                Program.WriteColoredLine($"[{DateTime.Now.ToString("HH:mm:ss")}] RCV Message not registered [] -> {message}", ConsoleColor.Yellow);
                return;
            }

            Program.WriteColoredLine($"[{DateTime.Now.ToString("HH:mm:ss")}] RCV [Name : {packetname} | Type : {Type["type"]}] -> {json}", ConsoleColor.DarkCyan);
            foreach (var method in Methods.Where(x => x.Key == Type["type"].ToString())) // on cherche si il y a un handler avec le meme nom de message
            {
                method.Methode.Invoke(method.Instance, new object[] { client, MessageBuilder.GetMessage(Type["type"].ToString(), Type) });
            }

            //En gros il faut faire une classe objet pour chaque type et la call a partir du type trouvé
            // une fois qu'on get lobjet rempli, on peut call l'update si il existe en utilisant les datas qu'on a
        }
    }
}