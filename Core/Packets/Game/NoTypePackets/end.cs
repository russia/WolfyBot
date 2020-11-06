using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WolfyBot.Core.Types;

namespace WolfyBot.Core.Packets.Game.NoTypePackets
{
    public class end : Message
    {
        [JsonProperty("currentXp")]
        public int CurrentXp { get; set; }

        [JsonProperty("currentMoons")]
        public int CurrentMoons { get; set; }

        [JsonProperty("currentCoins")]
        public int CurrentCoins { get; set; }

        [JsonProperty("currentElo")]
        public int CurrentElo { get; set; }

        [JsonProperty("points")]
        public Points Points { get; set; }

        [JsonProperty("moons")]
        public int Moons { get; set; }

        [JsonProperty("coins")]
        public int Coins { get; set; }

        [JsonProperty("elo")]
        public int Elo { get; set; }

        [JsonProperty("reason")]
        public string Reason { get; set; }

        [JsonProperty("players")]
        public List<Player> Players { get; set; }
    }
}
