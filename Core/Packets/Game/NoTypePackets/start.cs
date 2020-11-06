using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WolfyBot.Core.Packets.Game.NoTypePackets
{
    public class start : Message
    {
        [JsonProperty("role")]
        public string Role { get; set; }

        [JsonProperty("werewolvesId")]
        public List<string> WerewolvesId { get; set; }
    }
}
