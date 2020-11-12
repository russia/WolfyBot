using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WolfyBot.Core.Types
{
    public class Game
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("instanceId")]
        public string InstanceId { get; set; }

        [JsonProperty("status")]
        public int Status { get; set; }

        [JsonProperty("playerCount")]
        public int PlayerCount { get; set; }

        [JsonProperty("settings")]
        public Settings Settings { get; set; }

        [JsonProperty("private")]
        public bool Private { get; set; }

        [JsonProperty("voice")]
        public bool Voice { get; set; }

        [JsonProperty("serious")]
        public bool Serious { get; set; }

        [JsonProperty("platform")]
        public string Platform { get; set; }

        [JsonProperty("lang")]
        public string Lang { get; set; }

        [JsonProperty("createdAt")]
        public DateTime CreatedAt { get; set; }

        [JsonProperty("updatedAt")]
        public DateTime UpdatedAt { get; set; }

        [JsonProperty("nextId")]
        public object NextId { get; set; }

        [JsonProperty("adminId")]
        public string AdminId { get; set; }

        [JsonProperty("admin")]
        public Admin Admin { get; set; }
    }
}
