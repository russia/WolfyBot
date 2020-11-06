using System;
using WolfyBot.Core.Dispatcher;

namespace WolfyBot.Core.Frames.GameFrames
{
    public class Death
    {
        [MessageAttribute("death", "voteVillagers")]
        public void HandlevoteVillagersType(Client client, WolfyBot.Core.Packets.Game.death.voteVillagers message)
        {
            Program.WriteColoredLine($"[{DateTime.Now.ToString("HH:mm:ss")}] [Day {message.Reason.DayNumber}] {message.Victims.Count} players are dead. Reason : {message.Reason.Type}", ConsoleColor.Blue);
        }

        [MessageAttribute("death", "lover")]
        public void HandleloverType(Client client, WolfyBot.Core.Packets.Game.death.lover message)
        {
            Program.WriteColoredLine($"[{DateTime.Now.ToString("HH:mm:ss")}] [Day {message.Reason.DayNumber}] {message.Victims.Count} players are dead. Reason : {message.Reason.Type}", ConsoleColor.Blue);
        }

        [MessageAttribute("death", "night")]
        public void HandlenightType(Client client, WolfyBot.Core.Packets.Game.death.night message)
        {
            Program.WriteColoredLine($"[{DateTime.Now.ToString("HH:mm:ss")}] [Day {message.Reason.DayNumber}] {message.Victims.Count} players are dead. Reason : {message.Reason.Type}", ConsoleColor.Blue);
        }

        [MessageAttribute("death", "mayorKill")]
        public void HandlemayorKillType(Client client, WolfyBot.Core.Packets.Game.death.mayorKill message)
        {
            Program.WriteColoredLine($"[{DateTime.Now.ToString("HH:mm:ss")}] [Day {message.Reason.DayNumber}] {message.Victims.Count} players are dead. Reason : {message.Reason.Type}", ConsoleColor.Blue);
        }
    }
}