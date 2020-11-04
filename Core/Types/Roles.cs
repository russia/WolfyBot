using Newtonsoft.Json;

namespace WolfyBot.Core.Types
{
    public class Roles
    {
        [JsonProperty("werewolf")]
        public int Werewolf { get; set; }

        [JsonProperty("blackWolf")]
        public int BlackWolf { get; set; }

        [JsonProperty("talkativeWolf")]
        public int TalkativeWolf { get; set; }

        [JsonProperty("whiteWolf")]
        public int WhiteWolf { get; set; }

        [JsonProperty("sickRat")]
        public int SickRat { get; set; }

        [JsonProperty("mercenary")]
        public int Mercenary { get; set; }

        [JsonProperty("villager")]
        public int Villager { get; set; }

        [JsonProperty("seer")]
        public int Seer { get; set; }

        [JsonProperty("witch")]
        public int Witch { get; set; }

        [JsonProperty("littleGirl")]
        public int LittleGirl { get; set; }

        [JsonProperty("hunter")]
        public int Hunter { get; set; }

        [JsonProperty("guard")]
        public int Guard { get; set; }

        [JsonProperty("cupid")]
        public int Cupid { get; set; }

        [JsonProperty("mentalist")]
        public int Mentalist { get; set; }

        [JsonProperty("necromencer")]
        public int Necromencer { get; set; }

        [JsonProperty("gravedigger")]
        public int Gravedigger { get; set; }

        [JsonProperty("dictator")]
        public int Dictator { get; set; }

        [JsonProperty("redRidingHood")]
        public int RedRidingHood { get; set; }

        [JsonProperty("pyromancer")]
        public int Pyromancer { get; set; }

        [JsonProperty("heir")]
        public int Heir { get; set; }
    }
}