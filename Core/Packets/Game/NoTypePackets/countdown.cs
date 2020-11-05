using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WolfyBot.Core.Packets.Game.NoTypePackets
{
    public class countdown : Message
    {
        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("timeLeft")]
        public int TimeLeft { get; set; }

        [JsonProperty("initialTime")]
        public int InitialTime { get; set; }
    }
}
