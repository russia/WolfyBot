using System;
using WolfyBot.Core.Dispatcher;
using WolfyBot.Core.Packets.Game.chat;

namespace WolfyBot.Core.Frames.GameFrames
{
    public class Chat
    {
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

        [MessageAttribute("chat", "userMessage")]
        public void HandleuserMessageType(Client client, userMessage message)
        {
            Program.WriteColoredLine($"[{DateTime.Now.ToString("HH:mm:ss")}] [{message.channel}] -> {message.userId} -> {message.text}", ConsoleColor.Blue);
        }

        [MessageAttribute("chat", "accusation")]
        public void HandleaccusationType(Client client, accusation message)
        {
            Program.WriteColoredLine($"[{DateTime.Now.ToString("HH:mm:ss")}] Player {message.VoterId} is accusating {message.TargetId} for {message.Text} reason.", ConsoleColor.Blue);
        }

        [MessageAttribute("chat", "start")]
        public void HandlestartType(Client client, WolfyBot.Core.Packets.Game.chat.start message)
        {
            Program.WriteColoredLine($"[{DateTime.Now.ToString("HH:mm:ss")}] The game is starting.", ConsoleColor.Blue);
        }

        [MessageAttribute("chat", "invoke")]
        public void HandleinvokeType(Client client, WolfyBot.Core.Packets.Game.chat.invoke message)
        {
            Program.WriteColoredLine($"[{DateTime.Now.ToString("HH:mm:ss")}] It's {message.Role} turn.", ConsoleColor.Blue);
        }

        [MessageAttribute("chat", "info")]
        public void HandleinfoType(Client client, WolfyBot.Core.Packets.Game.chat.info message)
        {
            Program.WriteColoredLine($"[{DateTime.Now.ToString("HH:mm:ss")}] Info message : {message.Message}, multiple : {message.Multiple}", ConsoleColor.Blue);
        }

        [MessageAttribute("chat", "vote")]
        public void HandlevoteType(Client client, WolfyBot.Core.Packets.Game.chat.vote message)
        {
            Program.WriteColoredLine($"[{DateTime.Now.ToString("HH:mm:ss")}] [{message.Channel}] {message.VoterId} is voting for {message.TargetId}", ConsoleColor.Blue);
        }

        [MessageAttribute("chat", "mayorPresentation")]
        public void HandlemayorPresentationType(Client client, WolfyBot.Core.Packets.Game.chat.mayorPresentation message)
        {
            Program.WriteColoredLine($"[{DateTime.Now.ToString("HH:mm:ss")}] [{message.Channel}] {message.PlayerId} is presenting for {message.Type}. Arguments : {message.Text}", ConsoleColor.Blue);
        }

        [MessageAttribute("chat", "mayorVote")]
        public void HandlemayorVoteType(Client client, WolfyBot.Core.Packets.Game.chat.mayorVote message)
        {
            Program.WriteColoredLine($"[{DateTime.Now.ToString("HH:mm:ss")}] [{message.Channel}] {message.VoterId} is voting for {message.TargetId} to be {message.Type}.", ConsoleColor.Blue);
        }

        [MessageAttribute("chat", "dayChange")]
        public void HandledayChangeType(Client client, WolfyBot.Core.Packets.Game.chat.dayChange message)
        {
            Program.WriteColoredLine($"[{DateTime.Now.ToString("HH:mm:ss")}] [{message.Channel}] New day number : {message.DayNumber}.", ConsoleColor.Blue);
        }

        [MessageAttribute("chat", "death")]
        public void HandledeathType(Client client, WolfyBot.Core.Packets.Game.chat.death message)
        {
            //on handle deja les msg de morts
            //  Program.WriteColoredLine($"[{DateTime.Now.ToString("HH:mm:ss")}] It's {message.Role} turn.", ConsoleColor.Blue);
        }
    }
}