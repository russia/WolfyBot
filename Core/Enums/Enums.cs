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

    public enum GameSide
    {
        SOLO,
        DUO,
        WEREWOLVES,  
        VILLAGERS
    }
}