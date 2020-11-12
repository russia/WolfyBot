using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using WolfyBot.Core.Types;

namespace WolfyBot.Core.Game.Types
{
    public class GameAction
    {
        public string ActionID { get; set; }
        public bool IsEnded { get; set; }
        public int Timeleft { get; set; }

        public string ActionType { get; set; }
        public bool canProcessAction { get; set; }
        public System.Timers.Timer ActionTimer = new System.Timers.Timer();
        public Info Informations { get; set; }

        public int DayNumber { get; set; }

        public GameAction (string actionID,string actionType, int timeleft = 0)
        {
            DayNumber = 0; 
            canProcessAction = false;
            this.ActionID = actionID;
            this.ActionType = actionType;
            ActionTimer.Elapsed += new ElapsedEventHandler(OnTimedGameActionEvent);
            ActionTimer.Interval = timeleft - 10000;
            ActionTimer.Enabled = true;
        }
        private void OnTimedGameActionEvent(object source, ElapsedEventArgs e)
        {
            Program.WriteColoredLine("OnTimedGameActionEvent ! Processing action..", ConsoleColor.Green);
            canProcessAction = true;
            ActionTimer.Enabled = false;
        }

    }
}
