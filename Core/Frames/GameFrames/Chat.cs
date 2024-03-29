﻿using System;
using System.Linq;
using WolfyBot.Core.Dispatcher;
using WolfyBot.Core.Packets.Game.chat;

namespace WolfyBot.Core.Frames.GameFrames
{
    public class Chat
    {
        [MessageAttribute("chat", "join")]
        public void HandlejoinchatMessage(Client client, WolfyBot.Core.Packets.Game.chat.join message)
        {
            Program.WriteColoredLine($"[{DateTime.Now.ToString("HH:mm:ss")}] {message.userId} joined {message.channel} chat", ConsoleColor.Blue, client);
        }

        [MessageAttribute("chat", "leave")]
        public void HandleleavechatMessage(Client client, WolfyBot.Core.Packets.Game.chat.leave message)
        {
            Program.WriteColoredLine($"[{DateTime.Now.ToString("HH:mm:ss")}] {message.UserId} leave {message.Channel} chat", ConsoleColor.Blue, client);
            try
            {
                client.InGameIA.PartyPlayers.Remove(client.InGameIA.PartyPlayers.First(x => x.Id == message.UserId));
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); }
        }

        [MessageAttribute("chat", "userMessage")]
        public void HandleuserMessageType(Client client, userMessage message)
        {
            Program.WriteColoredLine($"[{DateTime.Now.ToString("HH:mm:ss")}] [{message.channel}] -> {message.userId} -> {message.text}", ConsoleColor.Blue, client);
        }

        [MessageAttribute("chat", "accusation")]
        public void HandleaccusationType(Client client, accusation message)
        {
            Program.WriteColoredLine($"[{DateTime.Now.ToString("HH:mm:ss")}] Player {message.VoterId} is accusating {message.TargetId} for {message.Text} reason.", ConsoleColor.Blue, client);
            try
            {
                if (message.TargetId != client.Userid)
                    client.InGameIA.AccusatedPlayers.Add(client.InGameIA.PartyPlayers.First(x => x.Id == message.TargetId));
            }
            catch { }
        }

        [MessageAttribute("chat", "start")]
        public void HandlestartType(Client client, WolfyBot.Core.Packets.Game.chat.start message)
        {
            Program.WriteColoredLine($"[{DateTime.Now.ToString("HH:mm:ss")}] The game is starting.", ConsoleColor.Blue, client);
        }

        [MessageAttribute("chat", "invoke")]
        public void HandleinvokeType(Client client, WolfyBot.Core.Packets.Game.chat.invoke message)
        {
            Program.WriteColoredLine($"[{DateTime.Now.ToString("HH:mm:ss")}] It's {message.Role} turn.", ConsoleColor.Blue, client);
            //if(message.Role == "werewolf")
            //    client.SendMessage("42[\"chat\",{\"text\":\"ripp\",\"private\":false}]");
        }

        [MessageAttribute("chat", "info")]
        public void HandleinfoType(Client client, WolfyBot.Core.Packets.Game.chat.info message)
        {
            Program.WriteColoredLine($"[{DateTime.Now.ToString("HH:mm:ss")}] Info message : {message.Message}, multiple : {message.Multiple}", ConsoleColor.Blue, client);
            if (message.Word != null)
                client.InGameIA.ProcessAction(message);
            if (message.TargetId != null)
                client.InGameIA.ProcessAction(message);
        }

        [MessageAttribute("chat", "vote")]
        public void HandlevoteType(Client client, WolfyBot.Core.Packets.Game.chat.vote message)
        {
            Program.WriteColoredLine($"[{DateTime.Now.ToString("HH:mm:ss")}] [{message.Channel}] {message.VoterId} is voting for {message.TargetId}", ConsoleColor.Blue, client);
        }

        [MessageAttribute("chat", "mayorPresentation")]
        public void HandlemayorPresentationType(Client client, WolfyBot.Core.Packets.Game.chat.mayorPresentation message)
        {
            Program.WriteColoredLine($"[{DateTime.Now.ToString("HH:mm:ss")}] [{message.Channel}] {message.PlayerId} is presenting for {message.Type}. Arguments : {message.Text}", ConsoleColor.Blue, client);
            try
            {
                if (message.PlayerId != client.Userid)
                    client.InGameIA.MayorCandidates.Add(client.InGameIA.PartyPlayers.First(x => x.Id == message.PlayerId));
            }
            catch { }
        }

        [MessageAttribute("chat", "mayorVote")]
        public void HandlemayorVoteType(Client client, WolfyBot.Core.Packets.Game.chat.mayorVote message)
        {
            Program.WriteColoredLine($"[{DateTime.Now.ToString("HH:mm:ss")}] [{message.Channel}] {message.VoterId} is voting for {message.TargetId} to be {message.Type}.", ConsoleColor.Blue, client);
        }

        [MessageAttribute("chat", "dayChange")]
        public void HandledayChangeType(Client client, WolfyBot.Core.Packets.Game.chat.dayChange message)
        {
            Program.WriteColoredLine($"[{DateTime.Now.ToString("HH:mm:ss")}] [{message.Channel}] New day number : {message.DayNumber}.", ConsoleColor.Blue, client);
            client.InGameIA.CurrentDayCount = message.DayNumber;
            // client.InGameIA.SendWord();
        }

        [MessageAttribute("chat", "death")]
        public void HandledeathType(Client client, WolfyBot.Core.Packets.Game.chat.death message)
        {
            //on handle deja les msg de morts
            //  Program.WriteColoredLine($"[{DateTime.Now.ToString("HH:mm:ss")}] It's {message.Role} turn.", ConsoleColor.Blue);
        }
    }
}