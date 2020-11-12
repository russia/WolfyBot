using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WolfyBot.Core.Types;

namespace WolfyBot.Core.Packets.Hub.NoTypePackets
{
    public class hydrate : Message
    {
        [JsonProperty("currentGame")]
        public object CurrentGame { get; set; }

        [JsonProperty("games")]
        public List<WolfyBot.Core.Types.Game> Games { get; set; }

        [JsonProperty("userCount")]
        public int UserCount { get; set; }

        [JsonProperty("onlineCount")]
        public int OnlineCount { get; set; }

        [JsonProperty("chests")]
        public List<object> Chests { get; set; }

        [JsonProperty("notifications")]
        public List<Notification> Notifications { get; set; }
    }
}
