using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WolfyBot.Core.Types
{
    public class Data
    {
        [JsonProperty("reason")]
        public string Reason { get; set; }

        [JsonProperty("text")]
        public string Text { get; set; }
    }
}
