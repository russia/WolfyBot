using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WolfyBot.Core.Types
{
    public class Notification
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("seen")]
        public bool Seen { get; set; }

        [JsonProperty("createdAt")]
        public DateTime CreatedAt { get; set; }

        [JsonProperty("updatedAt")]
        public DateTime UpdatedAt { get; set; }

        [JsonProperty("userId")]
        public string UserId { get; set; }

        [JsonProperty("contentId")]
        public int ContentId { get; set; }

        [JsonProperty("content")]
        public Content Content { get; set; }
    }
}
