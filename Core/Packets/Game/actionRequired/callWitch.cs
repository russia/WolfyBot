using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WolfyBot.Core.Packets.Game.actionRequired
{
    public class callWitch : Message
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("healPotionUsed")]
        public bool HealPotionUsed { get; set; }

        [JsonProperty("deathPotionUsed")]
        public bool DeathPotionUsed { get; set; }

        [JsonProperty("victimId")]
        public string VictimId { get; set; }

        [JsonProperty("timeLeft")]
        public int TimeLeft { get; set; }
    }
}
