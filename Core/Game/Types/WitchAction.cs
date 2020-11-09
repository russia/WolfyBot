using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WolfyBot.Core.Game.Types
{
    public class WitchAction
    {
        public enum type
        {
            death,
            heal,
            none,
        }
        public type ActionType { get; set; }
        public string TargetId { get; set; }
        public WitchAction(string targetId, type actiontype)
        {
            this.ActionType = actiontype;
            this.TargetId = targetId;
        }
    }
}
