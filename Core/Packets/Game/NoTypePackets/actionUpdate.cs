using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WolfyBot.Core.Types;

namespace WolfyBot.Core.Packets.Game.NoTypePackets
{
    public class actionUpdate : Message
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("info")]
        public Info Info { get; set; }

        [JsonProperty("timeLeft")]
        public int TimeLeft { get; set; }
    }
}
