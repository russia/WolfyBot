using System;
using System.Linq;
using WolfyBot.Core.Dispatcher;
using WolfyBot.Core.Packets.Game.playerUpdate;

namespace WolfyBot.Core.Frames.GameFrames
{
    public class playerUpdate
    {
        [MessageAttribute("playerUpdate", "setInLove")]
        public void HandlesetInLoveType(Client client, WolfyBot.Core.Packets.Game.playerUpdate.setInLove message)
        {
            if (message.Role == null)
                return;
            Program.WriteColoredLine($"[{DateTime.Now.ToString("HH:mm:ss")}] You are in love with {message.UserId} his role : {message.Role}.", ConsoleColor.Blue, client);
            client.InGameIA.InLoveId = client.InGameIA.PartyPlayers.First(x => x.Id == message.UserId);
            client.InGameIA.CurrentRole.Side = Enums.GameSide.DUO;
        }

        [MessageAttribute("playerUpdate", "online")]
        public void HandleonlineType(Client client, online message)
        {
            Program.WriteColoredLine($"[{DateTime.Now.ToString("HH:mm:ss")}] {message.UserId} -> Online[{message.Online}]", ConsoleColor.Blue, client);
        }

        [MessageAttribute("playerUpdate", "addWerewolf")]
        public void HandleaddWerewolfType(Client client, addWerewolf message)
        {
            Program.WriteColoredLine($"[{DateTime.Now.ToString("HH:mm:ss")}] {message.UserId} is now a wolf [{message.Type}]", ConsoleColor.Blue, client);
            if (message.UserId != client.Userid)
                client.InGameIA.AlliesIds.Add(client.InGameIA.PartyPlayers.First(x => x.Id == message.UserId));
        }

        [MessageAttribute("playerUpdate", "setSick")]
        public void HandlesetSickType(Client client, setSick message)
        {
            if (message.UserId == client.Userid)
                return;

            if (message.Value) // seq not matching
                client.InGameIA.SickRatInfected.Add(client.InGameIA.PartyPlayers.First(x => x.Id == message.UserId));
            else
                client.InGameIA.SickRatInfected.Remove(client.InGameIA.PartyPlayers.First(x => x.Id == message.UserId));
            Program.WriteColoredLine($"[{DateTime.Now.ToString("HH:mm:ss")}] {message.UserId} is now sick [{message.Type}]", ConsoleColor.Blue, client);
        }
    }
}