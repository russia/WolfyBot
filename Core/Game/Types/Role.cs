using WolfyBot.Core.Enums;

namespace WolfyBot.Core.Game.Types
{
    public class Role
    {
        private string RoleName { get; set; }
        public GameSide Side { get; set; }

        public Role(string rolename, string side)
        {
            this.RoleName = rolename;
            switch (side)
            {
                case "solo":
                    this.Side = GameSide.SOLO;
                    break;
                case "werewolves":
                    this.Side = GameSide.WEREWOLVES;
                    break;
                case "villagers":
                    this.Side = GameSide.VILLAGERS;
                    break;
            }
            
        }
    }
}