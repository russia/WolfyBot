using Newtonsoft.Json;

namespace WolfyBot.Core.Packets.Hub.game_update
{
    public class UPDATE_STATUS : Message
    {
        public string Id { get; set; }

        public string Type { get; set; }

        public int Value { get; set; }

        public bool Serious { get; set; }

        public UPDATE_STATUS() { }

        public UPDATE_STATUS(string id, string type, int value, bool serious)
        {
            Id = id;
            Type = type;
            Value = value;
            Serious = serious;
        }
    }
}