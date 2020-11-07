using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WolfyBot.Core.Game.Types
{
    public class PlayerRole
    {
        public string Id { get; set; }
        public Role Role { get; set; }
        public PlayerRole(string id)
        {
            Id = id;
        }
    }
}
