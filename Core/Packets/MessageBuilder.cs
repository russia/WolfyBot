using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace WolfyBot.Core.Packets
{
    public static class MessageBuilder
    {
        private static readonly Dictionary<string, Type> _types = new Dictionary<string, Type>();

        public static void Initialize()
        {
            Program.WriteColoredLine("[MAIN] Initializing MessageBuilder...", ConsoleColor.Cyan);
            Assembly ass = Assembly.GetAssembly(typeof(Message));
            foreach (var type in ass.GetTypes())
            {
                if (!typeof(Message).IsAssignableFrom(type) || type == typeof(Message))
                    continue;

                var typeName = type.Name;
                _types.Add(typeName.ToLower(), type);
            }
        }

        public static Message GetMessage(string type, JObject data)
        {
            type = type.ToLower();
            if (!_types.ContainsKey(type))
                return null;

            var t = _types[type];
            Message result = null;
            try
            {
                result = data.ToObject(t) as Message; // cast le jobject en message
            }
            catch (Exception ex)
            {
                Program.WriteColoredLine($"unable to process message with type '{type}'\nerror : {ex.Message}\nfull message : \n{data}", ConsoleColor.Red);
            }

            return result;
        }
    }
}