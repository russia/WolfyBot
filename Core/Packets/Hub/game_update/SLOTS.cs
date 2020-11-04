using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WolfyBot.Core.Types;

namespace WolfyBot.Core.Packets.Hub.game_update
{
    public class SLOTS : Message
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("slots")]
        public int Slots { get; set; }

        [JsonProperty("roles")]
        public Roles Roles { get; set; }


    }
}
