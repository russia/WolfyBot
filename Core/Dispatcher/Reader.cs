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

            foreach (var type in assembly.GetTypes().SelectMany(x => x.GetMethods()).Where(m => m.GetCustomAttributes(typeof(MessageAttribute), false).Length > 0).ToArray()) // on cherche tout les types qui contiennent des methodes avec l'attribute Message
            {
                var Attributes = type.GetCustomAttributes(typeof(MessageAttribute), true).Select(x => (MessageAttribute)x); // on get les attributes de la methode
                foreach (var attr in Attributes)
                {
                    Type stringType = Type.GetType(type.DeclaringType.FullName); // on get le Type de la methode depuis sa classe

                    var instance = Activator.CreateInstance(stringType, null); // on cree une instance de la methode

                    Methods.Add(new PacketData(instance, attr.Message, attr.runSynchronously, type, attr.Type));// on save les donnees de la methode
                }
            }
        }

        public static void MessageReader(Client client, string message)
        {
            message = WolfyBot.Core.Helper.Extensions.CleanPacket("[", "]", message);
            string packetname = message.Substring(2, message.IndexOf(",{") - 3);
            string json = message.Replace($"[\"{packetname}\",", "");
            json = json.Remove(json.Length - 1);

            var Type = JObject.Parse(json);

            if (Type["type"] == null)
            {
                if (!Methods.Any(x => x.Messagename == packetname))
                {
                    Program.AddUnknowMsg(message + "\n\n");
                    Program.WriteColoredLine($"[{DateTime.Now.ToString("HH:mm:ss")}] RCV Message not registered [Name : {packetname} | Type : NOTYPE] -> {json}", ConsoleColor.Yellow);
                    return;
                }
                foreach (var method in Methods.Where(x => x.Messagename == packetname)) // on cherche si il y a un handler avec le meme nom de message
                {
                    method.Methode.Invoke(method.Instance, new object[] { client, MessageBuilder.GetMessage(Type, packetname) });
                }
                return;
            }

            if (!Methods.Where(x => x.Typename == Type["type"].ToString()).Any())
            {
                Program.AddUnknowMsg(message + "\n\n");
                Program.WriteColoredLine($"[{DateTime.Now.ToString("HH:mm:ss")}] RCV Message not registered [Type : {Type["type"]}] -> {message}", ConsoleColor.Yellow);
                return;
            }

            Program.WriteColoredLine($"[{DateTime.Now.ToString("HH:mm:ss")}] RCV [Name : {packetname} | Type : {Type["type"]}] -> {json}", ConsoleColor.DarkCyan);
            foreach (var method in Methods.Where(x => x.Typename == Type["type"].ToString())) // on cherche si il y a un handler avec le meme nom de message
            {
                method.Methode.Invoke(method.Instance, new object[] { client, MessageBuilder.GetMessage(Type, packetname, Type["type"].ToString()) });
            }
        }

        public static void StrangeMessageReader(Client client, string message)
        {
            Program.AddUnknowMsg(message + "\n\n");
            string packetname = message.Substring(4, message.IndexOf("\",") - 3);
            Console.WriteLine("Strange packet name parser ! " + packetname);
        }
    }
}