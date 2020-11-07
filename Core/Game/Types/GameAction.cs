using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WolfyBot.Core.Types;

namespace WolfyBot.Core.Game.Types
{
    public class GameAction
    {
        public string ActionID { get; set; }
        public bool IsEnded { get; set; }
        public int Timeleft { get; set; }

        public Info Informations { get; set; }

        public int DayNumber { get; set; }

        public GameAction (string actionID, int timeleft)
        {
            this.ActionID = actionID;
            this.Timeleft = timeleft;
        }

    }
}
