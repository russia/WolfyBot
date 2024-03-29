﻿using Newtonsoft.Json;

namespace WolfyBot.Core.Packets.Game.chat
{
    public class info : Message
    {
        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("channel")]
        public string Channel { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }

        [JsonProperty("multiple")]
        public bool Multiple { get; set; }

        [JsonProperty("recipientId")]
        public string RecipientId { get; set; }

        [JsonProperty("positive")]
        public bool Positive { get; set; }

        [JsonProperty("userId")]
        public string UserId { get; set; }

        [JsonProperty("word")]
        public string Word { get; set; }

        [JsonProperty("targetId")]
        public string TargetId { get; set; }
    }
}