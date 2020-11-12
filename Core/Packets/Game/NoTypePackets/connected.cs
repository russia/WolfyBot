using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WolfyBot.Core.Types;

namespace WolfyBot.Core.Packets.Game.NoTypePackets
{
    public class connected : Message
    {
        [JsonProperty("players")]
        public List<GamePlayer> Players { get; set; }

        [JsonProperty("settings")]
        public Settings Settings { get; set; }

        [JsonProperty("rolesLeft")]
        public RolesLeft RolesLeft { get; set; }

        [JsonProperty("messages")]
        public List<object> Messages { get; set; }

        [JsonProperty("status")]
        public int Status { get; set; }

        [JsonProperty("currentState")]
        public object CurrentState { get; set; }

        [JsonProperty("countdown")]
        public object Countdown { get; set; }

        [JsonProperty("isInfect")]
        public object IsInfect { get; set; }

        [JsonProperty("isLoversDead")]
        public bool IsLoversDead { get; set; }

        [JsonProperty("lastVotes")]
        public List<object> LastVotes { get; set; }

        [JsonProperty("suspicions")]
        public Suspicions Suspicions { get; set; }


    }
}
