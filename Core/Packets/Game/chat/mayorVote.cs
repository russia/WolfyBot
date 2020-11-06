using Newtonsoft.Json;

namespace WolfyBot.Core.Packets.Game.chat
{
    public class mayorVote : Message
    {
        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("channel")]
        public string Channel { get; set; }

        [JsonProperty("voterId")]
        public string VoterId { get; set; }

        [JsonProperty("targetId")]
        public string TargetId { get; set; }
    }
}
