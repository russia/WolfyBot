using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WolfyBot.Core.Types;

namespace WolfyBot.Core.Packets.Hub.NoTypePackets
{
    public class game_create : Message
    {
        public string id { get; set; }
        public string instanceId { get; set; }
        public int status { get; set; }
        public int playerCount { get; set; }
        public Settings settings { get; set; }
        public bool @private { get; set; }
        public bool voice { get; set; }
        public bool serious { get; set; }
        public string platform { get; set; }
        public string lang { get; set; }
        public DateTime createdAt { get; set; }
        public DateTime updatedAt { get; set; }
        public object nextId { get; set; }
        public string adminId { get; set; }
        public Admin admin { get; set; }
    }
}
