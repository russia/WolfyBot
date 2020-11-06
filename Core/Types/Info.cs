using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WolfyBot.Core.Types
{
    public class Info
    {
        [JsonProperty("votes")]
        public List<Vote> Votes { get; set; }

        [JsonProperty("targetsIds")]
        public List<string> TargetsIds { get; set; }
    }
}
