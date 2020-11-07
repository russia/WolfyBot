using System;
using System.Linq;
using WolfyBot.Core.Dispatcher;
using WolfyBot.Core.Game.Types;
using WolfyBot.Core.Packets.Game.playerUpdate;

namespace WolfyBot.Core.Frames.GameFrames
{
    public class playerUpdate
    {
        [MessageAttribute("playerUpdate", "setInLove")]
        public void HandlesetInLoveType(Client client, WolfyBot.Core.Packets.Game.playerUpdate.setInLove message)
        {
            if (message.Role != null)
            { // todo improve
                Program.WriteColoredLine($"[{DateTime.Now.ToString("HH:mm:ss")}] You are in love with {message.UserId} his role : {message.Role}.", ConsoleColor.Blue);
                client.InGameIA.InLoveId = new PlayerRole(message.UserId);
            }
        }
        [MessageAttribute("playerUpdate", "online")]
        public void HandleonlineType(Client client, online message)
        {
            Program.WriteColoredLine($"[{DateTime.Now.ToString("HH:mm:ss")}] {message.UserId} -> Online[{message.Online}]", ConsoleColor.Blue);
        }

        [MessageAttribute("playerUpdate", "addWerewolf")]
        public void HandleaddWerewolfType(Client client, addWerewolf message)
        {//TODO ADD HERE
            Program.WriteColoredLine($"[{DateTime.Now.ToString("HH:mm:ss")}] {message.UserId} is now a wolf [{message.Type}]", ConsoleColor.Blue);
        }
        [MessageAttribute("playerUpdate", "setSick")]
        public void HandlesetSickType(Client client, setSick message)
        {//TODO ADD HERE
            if (message.Value)
                client.InGameIA.SickRatInfected.Add(client.InGameIA.SickRatInfected.First(x => x.Id == message.UserId));
            else
                client.InGameIA.SickRatInfected.Remove(client.InGameIA.SickRatInfected.First(x => x.Id == message.UserId));
            Program.WriteColoredLine($"[{DateTime.Now.ToString("HH:mm:ss")}] {message.UserId} is now a wolf [{message.Type}]", ConsoleColor.Blue);
        }
    }
}