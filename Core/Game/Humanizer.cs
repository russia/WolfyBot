using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WolfyBot.Core.Game
{
    public static class Humanizer
    {
        public static int GetRandomInt(int a, int b)
        {
            Random rnd = new Random();
            return rnd.Next(a, b);
        }
        public static string CreatevoteVillagersSentence()
        {
            switch (GetRandomInt(1, 3))
            {
                case 1:
                    return "j'ai de bonnes infos";
                case 2:
                    return "j'ai pu voir certaines choses";
                case 3:
                    return "il a un comportement tres suspect";
            }
            return ""; 
        }

    }
}
