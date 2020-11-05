using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WolfyBot.Core.Packets.Game.chat
{
    public class join : Message
    {
        public string type { get; set; }
        public string channel { get; set; }
        public string userId { get; set; }
    }
}
