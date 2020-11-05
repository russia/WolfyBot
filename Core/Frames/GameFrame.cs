using System;
using WolfyBot.Core.Dispatcher;
using WolfyBot.Core.Packets.Game;
using WolfyBot.Core.Packets.Game.chat;
using WolfyBot.Core.Packets.Game.NoTypePackets;
using WolfyBot.Core.Packets.Game.playerUpdate;

namespace WolfyBot.Core.Frames
{
    public class GameFrame
    { //message = sans type -> Type = type d'un msg weird but idk
        [MessageAttribute("writing")]
        public void HandlewritingMessage(Client client, writing message)
        {
            Program.WriteColoredLine($"[{DateTime.Now.ToString("HH:mm:ss")}] {message.userId} is writing ..", ConsoleColor.Blue);
        }

        [MessageAttribute("chat", "join")]
        public void HandlejoinchatMessage(Client client, WolfyBot.Core.Packets.Game.chat.join message)
        {
            Program.WriteColoredLine($"[{DateTime.Now.ToString("HH:mm:ss")}] {message.userId} joined {message.channel} chat", ConsoleColor.Blue);
        }
        [MessageAttribute("chat", "leave")]
        public void HandleleavechatMessage(Client client, WolfyBot.Core.Packets.Game.chat.leave message)
        {
            Program.WriteColoredLine($"[{DateTime.Now.ToString("HH:mm:ss")}] {message.UserId} leave {message.Channel} chat", ConsoleColor.Blue);
        }
        [MessageAttribute("join")]
        public void HandlejoinGameMessage(Client client, WolfyBot.Core.Packets.Game.NoTypePackets.join message)
        {
            Program.WriteColoredLine($"[{DateTime.Now.ToString("HH:mm:ss")}] {message.User.Username} joined the game, rank : {message.User.Rank}", ConsoleColor.Blue);
        }
        [MessageAttribute("chat", "userMessage")]
        public void HandleuserMessageType(Client client, userMessage message)
        {
            Program.WriteColoredLine($"[{DateTime.Now.ToString("HH:mm:ss")}] [{message.channel}] -> {message.userId} -> {message.text}", ConsoleColor.Blue);
        }

        [MessageAttribute("playerUpdate", "online")]
        public void HandleonlineType(Client client, online message)
        {
            Program.WriteColoredLine($"[{DateTime.Now.ToString("HH:mm:ss")}] {message.UserId} -> Online[{message.Online}]", ConsoleColor.Blue);
        }
        [MessageAttribute("countdown")]
        public void HandlecountdownMessage(Client client, countdown message)
        {
            Program.WriteColoredLine($"[{DateTime.Now.ToString("HH:mm:ss")}] [Timer : {message.Title}] {message.TimeLeft/1000} seconds spent out of {message.InitialTime}.", ConsoleColor.Blue);
        }
        [MessageAttribute("settings")]
        public void HandlesettingsMessage(Client client, settings message)
        {
            Program.WriteColoredLine($"[{DateTime.Now.ToString("HH:mm:ss")}] Game owner updated game settings slots : {message.Slots}", ConsoleColor.Blue);
        }

        
    }
}