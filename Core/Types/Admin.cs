using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WolfyBot.Core.Types
{
    public class Admin
    {
        public string profilePicture { get; set; }
        public string id { get; set; }
        public string username { get; set; }
        public int xp { get; set; }
        public int rank { get; set; }
        public string skinVersion { get; set; }
        public int skinIndex { get; set; }
    }
}
