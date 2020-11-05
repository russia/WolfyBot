using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WolfyBot.Core.Types;

namespace WolfyBot.Core.Packets.Game.NoTypePackets
{
    public class settings : Message
    {
        [JsonProperty("slots")]
        public int Slots { get; set; }

        [JsonProperty("mayor")]
        public bool Mayor { get; set; }

        [JsonProperty("roles")]
        public Roles Roles { get; set; }

        [JsonProperty("voice")]
        public bool Voice { get; set; }

        [JsonProperty("balancing")]
        public int Balancing { get; set; }

        [JsonProperty("private")]
        public bool Private { get; set; }

        [JsonProperty("serious")]
        public bool Serious { get; set; }

        [JsonProperty("available")]
        public List<string> Available { get; set; }

        [JsonProperty("alpha")]
        public List<string> Alpha { get; set; }
    }
}
