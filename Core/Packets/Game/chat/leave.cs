using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WolfyBot.Core.Packets.Game.chat
{
    public class leave : Message
    {
        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("channel")]
        public string Channel { get; set; }

        [JsonProperty("userId")]
        public string UserId { get; set; }
    }
}
