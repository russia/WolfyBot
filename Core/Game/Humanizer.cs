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
        public static string CreateSayHelloSentence()
        {
            switch (GetRandomInt(1, 4))
            {
                case 1:
                    return "salut";
                case 2:
                    return "yop";
                case 3:
                    return "Bonjour";
                case 4:
                    return "slt";
            }
            return "";
        }
        public static string CreatevoteVillagersSentence()
        {
            switch (GetRandomInt(1, 4))
            {
                case 1:
                    return "j'ai de bonnes infos";
                case 2:
                    return "j'ai pu voir des choses";
                case 3:
                    return "il est suspect";
                case 4:
                    return "bon bah cest sur ducoup";
            }
            return ""; 
        }

    }
}
