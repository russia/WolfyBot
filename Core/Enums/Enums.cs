using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WolfyBot.Core.Enums
{
    public enum StatesEnum
    {
        NONE,
    }
    public enum NetworkEnum
    {
        LOGGING_IN,
        LOGGED_HUB,
        SWITCHING_TO_GAME,
        LOGGED_GAME,
        DISCONNECTED
    }
}
