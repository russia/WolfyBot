using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WolfyBot.Core.Types
{
    public class User
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("username")]
        public string Username { get; set; }

        [JsonProperty("profilePicture")]
        public string ProfilePicture { get; set; }

        [JsonProperty("rank")]
        public int Rank { get; set; }

        [JsonProperty("xp")]
        public int Xp { get; set; }

        [JsonProperty("skinVersion")]
        public string SkinVersion { get; set; }

        [JsonProperty("elo")]
        public int Elo { get; set; }

        [JsonProperty("linkedDiscord")]
        public bool LinkedDiscord { get; set; }

        [JsonProperty("skinIndex")]
        public int SkinIndex { get; set; }
    }
}
