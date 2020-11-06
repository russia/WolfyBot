using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WolfyBot.Core.Types
{
    public class Points
    {
        [JsonProperty("participation")]
        public int Participation { get; set; }

        [JsonProperty("guard")]
        public int Guard { get; set; }

        [JsonProperty("goodVote")]
        public int GoodVote { get; set; }
    }
}