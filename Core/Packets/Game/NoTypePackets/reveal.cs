using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WolfyBot.Core.Packets.Game.NoTypePackets
{
    public class reveal : Message
    {
        [JsonProperty("role")]
        public string Role { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }
    }
}
