using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WolfyBot.Core.Packets.Game.actionRequired
{
    public class callGuard : Message // selection du joueur a protéger
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("lastTargetId")]
        public string LastTargetId { get; set; }

        [JsonProperty("timeLeft")]
        public int TimeLeft { get; set; }
    }
}
