using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using WolfyBot.Core.Helper;

namespace WolfyBot.Core.Packets
{
    public abstract class Message
    {
        // Properties
        [JsonIgnore]
        public string MessageType => GetType().Name;
        
        public bool IsCall() => MessageType.EndsWith("Call");

        public string ToCall()
        {
            if (!IsCall())
                throw new System.Exception($"Type '{MessageType}' is not a Call");
            var callObj = new
            {
                call = MessageType.Replace("Call", "").ToCamelCase(),
                data = this
            };

            return SerializeWithCamelCase(callObj);
        }

        public string ToSendMessage()
        {
            if (IsCall())
                throw new System.Exception($"Type '{MessageType}' is not a Message");
            var sendMessageObj = new
            {
                call = "sendMessage",
                data = new
                {
                    type = MessageType,
                    data = this
                }
            };

            return SerializeWithCamelCase(sendMessageObj);
        }

        public static string SerializeWithCamelCase(object obj)
        {
            var settings = new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver(),
                Formatting = Formatting.None
            };

            return JsonConvert.SerializeObject(obj, settings);
        }
    }
}