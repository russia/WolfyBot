using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WolfyBot.Core.Types;

namespace WolfyBot.Core.Packets.Game.NoTypePackets
{
    public class join : Message
    {
        [JsonProperty("user")]
        public User User { get; set; }

        [JsonProperty("isMayor")]
        public bool IsMayor { get; set; }

        [JsonProperty("isOnline")]
        public bool IsOnline { get; set; }

        [JsonProperty("isAlive")]
        public bool IsAlive { get; set; }

        [JsonProperty("deathReason")]
        public object DeathReason { get; set; }

        [JsonProperty("isAdmin")]
        public bool IsAdmin { get; set; }

        [JsonProperty("role")]
        public object Role { get; set; }

        [JsonProperty("testator")]
        public object Testator { get; set; }

        [JsonProperty("isWerewolf")]
        public object IsWerewolf { get; set; }

        [JsonProperty("isSick")]
        public bool IsSick { get; set; }

        [JsonProperty("isInLove")]
        public bool IsInLove { get; set; }

        [JsonProperty("self")]
        public bool Self { get; set; }

    }
}
