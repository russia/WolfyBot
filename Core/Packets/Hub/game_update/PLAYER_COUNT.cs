using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WolfyBot.Core.Packets.Hub.game_update
{
    public class PLAYER_COUNT : Message
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("value")]
        public int Value { get; set; }

        [JsonProperty("serious")]
        public bool Serious { get; set; }
    }
}
