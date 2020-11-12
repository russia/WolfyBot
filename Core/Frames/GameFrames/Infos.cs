using System;
using System.Linq;
using WolfyBot.Core.Dispatcher;
using WolfyBot.Core.Enums;
using WolfyBot.Core.Game;
using WolfyBot.Core.Game.Types;
using WolfyBot.Core.Packets.Game.NoTypePackets;

namespace WolfyBot.Core.Frames.GameFrames
{
    public class Infos
    {
        [MessageAttribute("connected")]
        public void HandleconnectedMessage(Client client, connected message)
        {
            Program.WriteColoredLine($"[{DateTime.Now.ToString("HH:mm:ss")}] {message.Players.Count()} connected !", ConsoleColor.Blue, client);
            foreach (var player in message.Players)
            {
                if (player.User.Id != null && player.User.Id != client.Userid)
                {
                    Program.WriteColoredLine($"Adding {player.User.Id} in players list.", ConsoleColor.Green,client);
                    client.InGameIA.PartyPlayers.Add(new PlayerRole(player.User.Id));
                }
            }
        }

        [MessageAttribute("writing")]
        public void HandlewritingMessage(Client client, writing message)
        {
            //Program.WriteColoredLine($"[{DateTime.Now.ToString("HH:mm:ss")}] {message.userId} is writing ..", ConsoleColor.Blue);
        }

        [MessageAttribute("join")]
        public void HandlejoinGameMessage(Client client, WolfyBot.Core.Packets.Game.NoTypePackets.join message)
        {
            Program.WriteColoredLine($"[{DateTime.Now.ToString("HH:mm:ss")}] {message.User.Username} joined the game, elo : {message.User.Elo}", ConsoleColor.Blue, client);
            if (message.User.Id != client.Userid)
            {
                Program.WriteColoredLine($"Adding {message.User.Id} in players list.", ConsoleColor.Green,client);
                client.InGameIA.PartyPlayers.Add(new PlayerRole(message.User.Id));
            }
        }

        [MessageAttribute("countdown")]
        public void HandlecountdownMessage(Client client, countdown message)
        {
            Program.WriteColoredLine($"[{DateTime.Now.ToString("HH:mm:ss")}] [Timer : {message.Title}] {message.TimeLeft / 1000} seconds left out of {message.InitialTime / 1000}.", ConsoleColor.Blue, client);
        }

        [MessageAttribute("settings")]
        public void HandlesettingsMessage(Client client, settings message)
        {
            Program.WriteColoredLine($"[{DateTime.Now.ToString("HH:mm:ss")}] Game owner updated game settings slots : {message.Slots}", ConsoleColor.Blue, client);
        }

        [MessageAttribute("start")]
        public void HandlestartMessage(Client client, WolfyBot.Core.Packets.Game.NoTypePackets.start message)
        {
            client.InGameIA.isGameRunning = true;
            foreach (var player in client.InGameIA.PartyPlayers)
            {
                Console.WriteLine(player.Id);
            }
            Program.WriteColoredLine($"[{DateTime.Now.ToString("HH:mm:ss")}] The game is starting, your role : {message.Role}", ConsoleColor.Magenta, client);
            client.SendMessage("42[\"chat\",{\"text\":\"" + Humanizer.CreateSayHelloSentence() + "\",\"private\":false}]");
            try
            {
                client.InGameIA.PartyPlayers.Distinct();//anti beug du owner qui rejoint/quitte
            }
            catch { }
            client.InGameIA.SetGameRole(message.Role);
            if (message.WerewolvesId == null || !message.WerewolvesId.Any())
                return;

            foreach (var wolf in message.WerewolvesId)
            {
                if (wolf != client.Userid)
                    client.InGameIA.AlliesIds.Add(client.InGameIA.PartyPlayers.First(x => x.Id == wolf));
            }
        }

        [MessageAttribute("end")]
        public void HandleendMessage(Client client, end message)
        {
            client.TotalEloEarned += message.Elo;
            Program.WriteColoredLine($"[{DateTime.Now.ToString("HH:mm:ss")}] The game is done : goodvotes -> {message.Points.GoodVote} | participation -> {message.Points.Participation}", ConsoleColor.Blue, client);
            client.Reconnect();
        }

        [MessageAttribute("death")]
        public void HandledeathMessage(Client client, WolfyBot.Core.Packets.Game.NoTypePackets.death message)
        {
            Program.WriteColoredLine($"[{DateTime.Now.ToString("HH:mm:ss")}] [Day {message.Reason.DayNumber}] {message.Victims.Count} players are dead. Reason : {message.Reason.Type}", ConsoleColor.Blue, client);
            if (!message.Victims.Any())
                return;

            if (message.Victims.Any(x => IAHelper.GetRole(x.Role).Side == GameSide.WEREWOLVES || IAHelper.GetRole(x.Role).Side == GameSide.SOLO))
                client.SendMessage("42[\"chat\",{\"text\":\"gg\",\"private\":false}]");
            else
                client.SendMessage("42[\"chat\",{\"text\":\"rip\",\"private\":false}]");

            foreach (var victim in message.Victims)
            {
                try
                {
                    if (victim.Id == client.Userid)                   
                        client.Reconnect();                                          
                    else
                    {
                        Program.WriteColoredLine($"Removing {victim.Id} from players list.",ConsoleColor.Green,client);
                        client.InGameIA.PartyPlayers.Remove(client.InGameIA.PartyPlayers.First(x => x.Id == victim.Id));
                    }
                        
                }
                catch (Exception ex)
                {
                    Console.WriteLine("[MessageAttribute(death)] " + ex.Message);
                }
            }
        }

        [MessageAttribute("reveal")]
        public void HandlerevealMessage(Client client, WolfyBot.Core.Packets.Game.NoTypePackets.reveal message)
        {
            Program.WriteColoredLine($"[{DateTime.Now.ToString("HH:mm:ss")}] {message.Id} revealed his role, he was {message.Role}.", ConsoleColor.Blue, client);
        }
    }
}