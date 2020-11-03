using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WolfyBot.Core.Packets.Hub
{
    public class game_create : Message
    {
        public class Roles
        {
            [JsonProperty("werewolf")]
            public int Werewolf { get; set; }

            [JsonProperty("villager")]
            public int Villager { get; set; }

            [JsonProperty("seer")]
            public int Seer { get; set; }

            [JsonProperty("witch")]
            public int Witch { get; set; }

            [JsonProperty("hunter")]
            public int Hunter { get; set; }

            [JsonProperty("guard")]
            public int Guard { get; set; }

            [JsonProperty("cupid")]
            public int Cupid { get; set; }
        }

        public class Settings
        {
            [JsonProperty("slots")]
            public int Slots { get; set; }

            [JsonProperty("mayor")]
            public bool Mayor { get; set; }

            [JsonProperty("roles")]
            public Roles Roles { get; set; }
        }

        public class Admin
        {
            [JsonProperty("profilePicture")]
            public string ProfilePicture { get; set; }

            [JsonProperty("id")]
            public string Id { get; set; }

            [JsonProperty("username")]
            public string Username { get; set; }

            [JsonProperty("xp")]
            public int Xp { get; set; }

            [JsonProperty("rank")]
            public int Rank { get; set; }

            [JsonProperty("skinVersion")]
            public string SkinVersion { get; set; }

            [JsonProperty("skinIndex")]
            public int SkinIndex { get; set; }
        }

        public class Root
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
}
