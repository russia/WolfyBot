using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WolfyBot.Core.Helper.APIRequests.APIResponses
{
    public class GameInfo
    {
        public class Roles
        {
            [JsonProperty("seer")]
            public int Seer { get; set; }

            [JsonProperty("witch")]
            public int Witch { get; set; }

            [JsonProperty("blackWolf")]
            public int BlackWolf { get; set; }

            [JsonProperty("hunter")]
            public int Hunter { get; set; }

            [JsonProperty("werewolf")]
            public int Werewolf { get; set; }

            [JsonProperty("mentalist")]
            public int Mentalist { get; set; }

            [JsonProperty("cupid")]
            public int Cupid { get; set; }

            [JsonProperty("guard")]
            public int Guard { get; set; }

            [JsonProperty("whiteWolf")]
            public int WhiteWolf { get; set; }

            [JsonProperty("necromencer")]
            public int Necromencer { get; set; }

            [JsonProperty("littleGirl")]
            public int LittleGirl { get; set; }

            [JsonProperty("gravedigger")]
            public int Gravedigger { get; set; }

            [JsonProperty("dictator")]
            public int Dictator { get; set; }

            [JsonProperty("redRidingHood")]
            public int RedRidingHood { get; set; }
        }

        public class Settings
        {
            [JsonProperty("slots")]
            public int Slots { get; set; }

            [JsonProperty("mayor")]
            public bool Mayor { get; set; }

            [JsonProperty("roles")]
            public Roles Roles { get; set; }

            [JsonProperty("balancing")]
            public int Balancing { get; set; }
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
        }
    }
}
