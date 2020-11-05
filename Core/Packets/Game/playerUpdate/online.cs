using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WolfyBot.Core.Packets.Game.playerUpdate
{
    public class online : Message
    {
        [JsonProperty("userId")]
        public string UserId { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("online")]
        public bool Online { get; set; }
    }
}
