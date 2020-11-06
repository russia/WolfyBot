using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WolfyBot.Core.Types;

namespace WolfyBot.Core.Packets.Game.chat
{
    public class death : Message
    {
        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("channel")]
        public string Channel { get; set; }

        [JsonProperty("victims")]
        public List<Victim> Victims { get; set; }

        [JsonProperty("reason")]
        public Reason Reason { get; set; }
    }
}
