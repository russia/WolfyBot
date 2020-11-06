using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WolfyBot.Core.Types
{
    public class Vote
    {
        [JsonProperty("voterId")]
        public string VoterId { get; set; }

        [JsonProperty("targetId")]
        public string TargetId { get; set; }
    }
}
