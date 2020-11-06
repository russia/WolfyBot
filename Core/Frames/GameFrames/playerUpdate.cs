using System;
using WolfyBot.Core.Dispatcher;
using WolfyBot.Core.Packets.Game.playerUpdate;

namespace WolfyBot.Core.Frames.GameFrames
{
    public class playerUpdate
    {
        [MessageAttribute("playerUpdate", "setInLove")]
        public void HandlesetInLoveType(Client client, WolfyBot.Core.Packets.Game.playerUpdate.setInLove message)
        {
            if (message.Role != "")
                Program.WriteColoredLine($"[{DateTime.Now.ToString("HH:mm:ss")}] You are in love with {message.UserId} his role : {message.Role}.", ConsoleColor.Blue);
        }
        
        [MessageAttribute("playerUpdate", "online")]
        public void HandleonlineType(Client client, online message)
        {
            Program.WriteColoredLine($"[{DateTime.Now.ToString("HH:mm:ss")}] {message.UserId} -> Online[{message.Online}]", ConsoleColor.Blue);
        }

        [MessageAttribute("playerUpdate", "addWerewolf")]
        public void HandleaddWerewolfType(Client client, addWerewolf message)
        {
            Program.WriteColoredLine($"[{DateTime.Now.ToString("HH:mm:ss")}] {message.UserId} is now a wolf [{message.Type}]", ConsoleColor.Blue);
        }
    }
}