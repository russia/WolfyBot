using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WolfyBot.Core.Packets.Game.NoTypePackets
{
    public class writing : Message
    {
        public string userId { get; set; }
        public bool status { get; set; }
        public bool @private { get; set; }
    }
}
