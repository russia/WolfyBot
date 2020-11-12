using WolfyBot.Core.Enums;

namespace WolfyBot.Core.Game.Types
{
    public class Role
    {
        public string RoleName { get; set; }
        public GameSide Side { get; set; }
        public bool IsAlive { get; set; }
        public string targetId { get; set; }
        public string AltRoleName { get; set; }

        public Role(string rolename, string side)
        {
            this.IsAlive = true;
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