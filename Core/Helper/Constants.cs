namespace WolfyBot.Helper
{
    public class Constants
    {
        ////////////////GAME CONST////////////////
        public const string APIUrl = "https://wolfy.fr/api/";
        public const string APIGameUrl = APIUrl + "/game/"; // APIGameUrl + "gameid";
        public const string APIUserUrl = APIUrl + "/user/"; // Authorization: TOKEN in header
        public const string APIUserProfilPicture = APIUrl + "/skin/render/picture.webp?id="; // APIUserProfilPicture + "userid";
        public const string APIUserBodyPic = APIUrl + "https://wolfy.fr/api/skin/render/user.svg?id=5a8fdb16-6cce-4226-b692-e6cac706d889&v=7W7G3JHMJM&s=0"; // APIUserProfilPicture + "userid";
        public const string APIUserRank = APIUrl + "/leaderboard/rankings"; // Authorization: TOKEN in header
        public const string SocketUrl = "https://wolfy.fr/socket.io/"; // ?token=TOKEN&EIO=3&transport=polling&t=" + 7 A-Z 0-1 Rand string
        public const int MaxParallelGamesNumber = 40;
    }
}