using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace WolfyBot.Core.Packets
{
    public static class MessageBuilder
    {
        private static readonly List<Type> _types = new List<Type>();

        public static void Initialize()
        {
            Program.WriteColoredLine("[MAIN] Initializing MessageBuilder...", ConsoleColor.Cyan);
            Assembly ass = Assembly.GetAssembly(typeof(Message));
            foreach (var type in ass.GetTypes())
            {
                if (!typeof(Message).IsAssignableFrom(type) || type == typeof(Message))
                    continue;
                _types.Add(type);
            }
        }

        public static Message GetMessage(Client client,JObject data, string messagename, string type = null) //message name = game_update, types : player count, update status
        {
            Type obj = null;
            if (type != null && _types.Any(x => x.FullName.Contains(messagename + "." + type))) {
                var objs = _types.Where(x => x.FullName.Contains(messagename + "." + type));
                if (objs.Count() != 1)
                    Console.WriteLine();
                else
                    obj = objs.First();

            }
            else if (_types.Any(x => x.FullName.Contains("NoTypePackets." + messagename)))
            {
                var objs = _types.Where(x => x.FullName.Contains("NoTypePackets." + messagename));
                if (objs.Count() != 1)
                    Console.WriteLine();
                else
                    obj = objs.First();
            }
            else
            {
                Console.WriteLine("WTF THERE IS A BIG ISSUE");
                Console.ReadKey();
            }

            Message result = null;
            try
            {
                result = data.ToObject(obj) as Message; // cast le jobject en message
            }
            catch (Exception ex)
            {
                Program.WriteColoredLine($"unable to process message with type '{type}'\nerror : {ex.Message}\nfull message : \n{data}", ConsoleColor.Red, client);
            }

            return result;
        }
    }
}