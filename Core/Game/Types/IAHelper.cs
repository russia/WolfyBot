namespace WolfyBot.Core.Game.Types
{
    public static class IAHelper
    {
        public static Role GetRole(string Role)
        {
            switch (Role)
            {
                case "werewolf": //Wolfs
                case "blackWolf":
                case "talkativeWolf":
                    return new Role(Role, "werewolves");

                case "whiteWolf": //self
                case "sickRat":
                case "mercenary":
                    return new Role(Role, "solo");

                case "villager": //Village
                case "seer":
                case "witch":
                case "littleGirl":
                case "hunter":
                case "guard":
                case "cupid":
                case "mentalist":
                case "necromencer":
                case "gravedigger":
                case "dictator":
                case "redRidingHood":
                case "pyromancer":
                case "heir":
                    return new Role(Role, "villagers");
            }
            return null;
        }
    }
}