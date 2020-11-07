using System;
using System.Linq;
using WolfyBot.Core.Dispatcher;
using WolfyBot.Core.Game;
using WolfyBot.Core.Game.Types;
using WolfyBot.Core.Packets.Game.NoTypePackets;

namespace WolfyBot.Core.Frames.GameFrames
{
    public class Infos
    {
        [MessageAttribute("writing")]
        public void HandlewritingMessage(Client client, writing message)
        {
            Program.WriteColoredLine($"[{DateTime.Now.ToString("HH:mm:ss")}] {message.userId} is writing ..", ConsoleColor.Blue);
        }

        [MessageAttribute("join")]
        public void HandlejoinGameMessage(Client client, WolfyBot.Core.Packets.Game.NoTypePackets.join message)
        {
            Program.WriteColoredLine($"[{DateTime.Now.ToString("HH:mm:ss")}] {message.User.Username} joined the game, elo : {message.User.Elo}", ConsoleColor.Blue);
        }

        [MessageAttribute("countdown")]
        public void HandlecountdownMessage(Client client, countdown message)
        {
            Program.WriteColoredLine($"[{DateTime.Now.ToString("HH:mm:ss")}] [Timer : {message.Title}] {message.TimeLeft / 1000} seconds left out of {message.InitialTime / 1000}.", ConsoleColor.Blue);
        }

        [MessageAttribute("settings")]
        public void HandlesettingsMessage(Client client, settings message)
        {
            Program.WriteColoredLine($"[{DateTime.Now.ToString("HH:mm:ss")}] Game owner updated game settings slots : {message.Slots}", ConsoleColor.Blue);
        }

        [MessageAttribute("start")]
        public void HandlestartMessage(Client client, WolfyBot.Core.Packets.Game.NoTypePackets.start message)
        {
            Program.WriteColoredLine($"[{DateTime.Now.ToString("HH:mm:ss")}] The game is starting, your role : {message.Role}", ConsoleColor.Magenta);

            client.SendMessage("42[\"writing\",{\"status\":true,\"private\":false}]");
            client.SendMessage("42[\"writing\",{\"status\":false,\"private\":false}]");
            client.SendMessage("42[\"chat\",{\"text\":\"salut\",\"private\":false}]");


            client.InGameIA.SetGameRole(message.Role);
            if (message.WerewolvesId == null || !message.WerewolvesId.Any())
                return;

            foreach (var wolf in message.WerewolvesId)
                client.InGameIA.AlliesIds.Add(new PlayerRole(wolf));
            
        }

        [MessageAttribute("end")]
        public void HandleendMessage(Client client, end message)
        {
            client.TotalEloEarned += message.Elo;
            Program.WriteColoredLine($"[{DateTime.Now.ToString("HH:mm:ss")}] The game is done : goodvotes -> {message.Points.GoodVote} | participation -> {message.Points.Participation}", ConsoleColor.Blue);
            client.InGameIA.Dispose();
        }

        [MessageAttribute("death")]
        public void HandledeathMessage(Client client, WolfyBot.Core.Packets.Game.NoTypePackets.death message)
        {
            Program.WriteColoredLine($"[{DateTime.Now.ToString("HH:mm:ss")}] [Day {message.Reason.DayNumber}] {message.Victims.Count} players are dead. Reason : {message.Reason.Type}", ConsoleColor.Blue);
        }

        [MessageAttribute("reveal")]
        public void HandlerevealMessage(Client client, WolfyBot.Core.Packets.Game.NoTypePackets.reveal message)
        {
            Program.WriteColoredLine($"[{DateTime.Now.ToString("HH:mm:ss")}] {message.Id} revealed his role, he was {message.Role}.", ConsoleColor.Blue);
        }
    }
}