using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WolfyBot.Core.Packets.Game.chat
{
    public class userMessage : Message
    {
        public string type { get; set; }
        public string text { get; set; }
        public string userId { get; set; }
        public string channel { get; set; }
    }
}
