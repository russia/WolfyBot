using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WolfyBot.Core.Types
{
    public class Reason
    {
        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("votersIds")]
        public List<string> VotersIds { get; set; }

        [JsonProperty("dayNumber")]
        public int DayNumber { get; set; }
    }
}
