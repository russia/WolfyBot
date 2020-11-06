using System;
using WolfyBot.Core.Dispatcher;

namespace WolfyBot.Core.Frames.GameFrames
{
    public class Actions
    {
        [MessageAttribute("actionRequired", "voteVillagers")]
        public void HandlevoteVillagersType(Client client, WolfyBot.Core.Packets.Game.actionRequired.voteVillagers message)
        {
            Program.WriteColoredLine($"[{DateTime.Now.ToString("HH:mm:ss")}] Action required : {message.Type} | Timeleft : {message.TimeLeft}.", ConsoleColor.Green);
        }
        
        [MessageAttribute("actionRequired", "voteMayor")]
        public void HandlevoteMayorType(Client client, WolfyBot.Core.Packets.Game.actionRequired.voteMayor message)
        {
            Program.WriteColoredLine($"[{DateTime.Now.ToString("HH:mm:ss")}] Action required : {message.Type} | Timeleft : {message.TimeLeft}.", ConsoleColor.Green);
        }

        [MessageAttribute("actionRequired", "voteKick")]
        public void HandlevoteKickType(Client client, WolfyBot.Core.Packets.Game.actionRequired.voteKick message)
        {
            Program.WriteColoredLine($"[{DateTime.Now.ToString("HH:mm:ss")}] Action required : {message.Type} | Timeleft : {message.TimeLeft}.", ConsoleColor.Green);
        }

        [MessageAttribute("actionRequired", "callMayorKill")]
        public void HandlecallMayorKillType(Client client, WolfyBot.Core.Packets.Game.actionRequired.callMayorKill message)
        {
            Program.WriteColoredLine($"[{DateTime.Now.ToString("HH:mm:ss")}] Action required : {message.Type} | Timeleft : {message.TimeLeft}.", ConsoleColor.Green);
        }
        [MessageAttribute("actionRequired", "callDictatorAsk")]
        public void HandlecallDictatorAskType(Client client, WolfyBot.Core.Packets.Game.actionRequired.callDictatorAsk message)
        {
            Program.WriteColoredLine($"[{DateTime.Now.ToString("HH:mm:ss")}] Action required : {message.Type} | Timeleft : {message.TimeLeft}.", ConsoleColor.Green);
        }
        
        [MessageAttribute("actionRequired", "voteWerewolves")]
        public void HandlevoteWerewolvesType(Client client, WolfyBot.Core.Packets.Game.actionRequired.voteWerewolves message)
        { //TODO MOVE IN voteWerewolves CLASS
            Program.WriteColoredLine($"[{DateTime.Now.ToString("HH:mm:ss")}] Action required : {message.Type} | Timeleft : {message.TimeLeft}.", ConsoleColor.Green);
        }

        [MessageAttribute("actionRequired", "callBlackWolf")]
        public void HandlecallBlackWolfType(Client client, WolfyBot.Core.Packets.Game.actionRequired.callBlackWolf message)
        { //TODO MOVE IN callBlackWolf CLASS
            Program.WriteColoredLine($"[{DateTime.Now.ToString("HH:mm:ss")}] Action required : {message.Type} | Victim : {message.VictimId}.", ConsoleColor.Green);
        }

        [MessageAttribute("actionRequired", "callWitch")]
        public void HandlecallWitchType(Client client, WolfyBot.Core.Packets.Game.actionRequired.callWitch message)
        { //TODO MOVE IN callWitch CLASS
            Program.WriteColoredLine($"[{DateTime.Now.ToString("HH:mm:ss")}] Action required : {message.Type} | Timeleft : {message.TimeLeft}.", ConsoleColor.Green);
        }

        [MessageAttribute("actionRequired", "callSeer")]
        public void HandlecallSeerType(Client client, WolfyBot.Core.Packets.Game.actionRequired.callSeer message)
        { //TODO MOVE IN callSeer CLASS
            Program.WriteColoredLine($"[{DateTime.Now.ToString("HH:mm:ss")}] Action required : {message.Type} | Timeleft : {message.TimeLeft}.", ConsoleColor.Green);
        }

        [MessageAttribute("actionRequired", "callGuard")]
        public void HandlecallGuardType(Client client, WolfyBot.Core.Packets.Game.actionRequired.callGuard message)
        { //TODO MOVE IN GUARD CLASS
            Program.WriteColoredLine($"[{DateTime.Now.ToString("HH:mm:ss")}] Action required : {message.Type} | Timeleft : {message.TimeLeft}.", ConsoleColor.Green);
        }

        [MessageAttribute("actionRequired", "callCupid")]
        public void HandlecallCupidType(Client client, WolfyBot.Core.Packets.Game.actionRequired.callCupid message)
        { //TODO MOVE IN callCupid
            Program.WriteColoredLine($"[{DateTime.Now.ToString("HH:mm:ss")}] Action required : {message.Type} | Timeleft : {message.TimeLeft}.", ConsoleColor.Green);
        }

        [MessageAttribute("actionRequired", "callSickRat")]
        public void HandlecallSickRatType(Client client, WolfyBot.Core.Packets.Game.actionRequired.callSickRat message)
        { //TODO MOVE IN callSickRat
            Program.WriteColoredLine($"[{DateTime.Now.ToString("HH:mm:ss")}] Action required : {message.Type} | Timeleft : {message.TimeLeft}.", ConsoleColor.Green);
        }

        [MessageAttribute("actionUpdate")]
        public void HandleactionUpdateMessage(Client client, WolfyBot.Core.Packets.Game.NoTypePackets.actionUpdate message)
        {
            Program.WriteColoredLine($"[{DateTime.Now.ToString("HH:mm:ss")}] Action updated : {message.Id} | Timeleft : {message.TimeLeft}.", ConsoleColor.Green);
        }

        [MessageAttribute("actionEnd")]
        public void HandleactionEndMessage(Client client, WolfyBot.Core.Packets.Game.NoTypePackets.actionEnd message)
        {
            Program.WriteColoredLine($"[{DateTime.Now.ToString("HH:mm:ss")}] Action ended : {message.Id}.", ConsoleColor.Green);
        }
    }
}