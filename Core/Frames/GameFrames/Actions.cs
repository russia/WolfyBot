using System;
using System.Threading.Tasks;
using WolfyBot.Core.Dispatcher;

namespace WolfyBot.Core.Frames.GameFrames
{
    public class Actions
    {
        [MessageAttribute("actionRequired", "voteVillagers")]
        public void HandlevoteVillagersType(Client client, WolfyBot.Core.Packets.Game.actionRequired.voteVillagers message)
        {
            Program.WriteColoredLine($"[{DateTime.Now.ToString("HH:mm:ss")}] Action required : {message.Type} | Timeleft : {message.TimeLeft}.", ConsoleColor.Green, client);
            Task.Factory.StartNew(() => client.InGameIA.ProcessAction(message));
        }
        
        [MessageAttribute("actionRequired", "voteMayor")]
        public void HandlevoteMayorType(Client client, WolfyBot.Core.Packets.Game.actionRequired.voteMayor message)
        {
            Program.WriteColoredLine($"[{DateTime.Now.ToString("HH:mm:ss")}] Action required : {message.Type} | Timeleft : {message.TimeLeft}.", ConsoleColor.Green, client);
            Task.Factory.StartNew(() => client.InGameIA.ProcessAction(message));
        }

        [MessageAttribute("actionRequired", "voteKick")]
        public void HandlevoteKickType(Client client, WolfyBot.Core.Packets.Game.actionRequired.voteKick message)
        {
            Program.WriteColoredLine($"[{DateTime.Now.ToString("HH:mm:ss")}] Action required : {message.Type} | Timeleft : {message.TimeLeft}.", ConsoleColor.Green, client);
            Task.Factory.StartNew(() => client.InGameIA.ProcessAction(message));
        }

        [MessageAttribute("actionRequired", "callMayorKill")]
        public void HandlecallMayorKillType(Client client, WolfyBot.Core.Packets.Game.actionRequired.callMayorKill message)
        {
            Program.WriteColoredLine($"[{DateTime.Now.ToString("HH:mm:ss")}] Action required : {message.Type} | Timeleft : {message.TimeLeft}.", ConsoleColor.Green, client);
            Task.Factory.StartNew(() => client.InGameIA.ProcessAction(message));
        }
        [MessageAttribute("actionRequired", "callDictatorAsk")]
        public void HandlecallDictatorAskType(Client client, WolfyBot.Core.Packets.Game.actionRequired.callDictatorAsk message)
        {
            Program.WriteColoredLine($"[{DateTime.Now.ToString("HH:mm:ss")}] Action required : {message.Type} | Timeleft : {message.TimeLeft}.", ConsoleColor.Green, client);
            Task.Factory.StartNew(() => client.InGameIA.ProcessAction(message));
        }

        [MessageAttribute("actionRequired", "voteWerewolves")]
        public void HandlevoteWerewolvesType(Client client, WolfyBot.Core.Packets.Game.actionRequired.voteWerewolves message)
        { 
            Program.WriteColoredLine($"[{DateTime.Now.ToString("HH:mm:ss")}] Action required : {message.Type} | Timeleft : {message.TimeLeft}.", ConsoleColor.Green, client);
            Task.Factory.StartNew(() => client.InGameIA.ProcessAction(message));
        }
        [MessageAttribute("actionRequired", "callGravedigger")]
        public void HandlecallGravediggerType(Client client, WolfyBot.Core.Packets.Game.actionRequired.callGravedigger message)
        {
            Program.WriteColoredLine($"[{DateTime.Now.ToString("HH:mm:ss")}] Action required : {message.Type} | Timeleft : {message.TimeLeft}.", ConsoleColor.Green, client);
            Task.Factory.StartNew(() => client.InGameIA.ProcessAction(message));
        }

        [MessageAttribute("actionRequired", "callWhiteWolf")]
        public void HandlecallWhiteWolfType(Client client, WolfyBot.Core.Packets.Game.actionRequired.callWhiteWolf message)
        {
            Program.WriteColoredLine($"[{DateTime.Now.ToString("HH:mm:ss")}] Action required : {message.Type} | Timeleft : {message.TimeLeft}.", ConsoleColor.Green, client);
            Task.Factory.StartNew(() => client.InGameIA.ProcessAction(message));
        }

        [MessageAttribute("actionRequired", "callBlackWolf")]
        public void HandlecallBlackWolfType(Client client, WolfyBot.Core.Packets.Game.actionRequired.callBlackWolf message)
        { 
            Program.WriteColoredLine($"[{DateTime.Now.ToString("HH:mm:ss")}] Action required : {message.Type} | Victim : {message.VictimId}.", ConsoleColor.Green, client);
            Task.Factory.StartNew(() => client.InGameIA.ProcessAction(message));
        }

        [MessageAttribute("actionRequired", "callWitch")]
        public void HandlecallWitchType(Client client, WolfyBot.Core.Packets.Game.actionRequired.callWitch message)
        { 
            Program.WriteColoredLine($"[{DateTime.Now.ToString("HH:mm:ss")}] Action required : {message.Type} | Timeleft : {message.TimeLeft}.", ConsoleColor.Green, client);
            Task.Factory.StartNew(() => client.InGameIA.ProcessAction(message));
        }

        [MessageAttribute("actionRequired", "callSeer")]
        public void HandlecallSeerType(Client client, WolfyBot.Core.Packets.Game.actionRequired.callSeer message)
        {
            Program.WriteColoredLine($"[{DateTime.Now.ToString("HH:mm:ss")}] Action required : {message.Type} | Timeleft : {message.TimeLeft}.", ConsoleColor.Green, client);
            Task.Factory.StartNew(() => client.InGameIA.ProcessAction(message));
        }

        [MessageAttribute("actionRequired", "callHunter")]
        public void HandleHunterType(Client client, WolfyBot.Core.Packets.Game.actionRequired.callHunter message)
        {
            Program.WriteColoredLine($"[{DateTime.Now.ToString("HH:mm:ss")}] Action required : {message.Type} | Timeleft : {message.TimeLeft}.", ConsoleColor.Green, client);
            Task.Factory.StartNew(() => client.InGameIA.ProcessAction(message));
        }

        [MessageAttribute("actionRequired", "callGuard")]
        public void HandlecallGuardType(Client client, WolfyBot.Core.Packets.Game.actionRequired.callGuard message)
        {
            Program.WriteColoredLine($"[{DateTime.Now.ToString("HH:mm:ss")}] Action required : {message.Type} | Timeleft : {message.TimeLeft}.", ConsoleColor.Green, client);
            Task.Factory.StartNew(() => client.InGameIA.ProcessAction(message));
        }

        [MessageAttribute("actionRequired", "callCupid")]
        public void HandlecallCupidType(Client client, WolfyBot.Core.Packets.Game.actionRequired.callCupid message)
        { 
            Program.WriteColoredLine($"[{DateTime.Now.ToString("HH:mm:ss")}] Action required : {message.Type} | Timeleft : {message.TimeLeft}.", ConsoleColor.Green, client);
            Task.Factory.StartNew(() => client.InGameIA.ProcessAction(message));
        }

        [MessageAttribute("actionRequired", "callSickRat")]
        public void HandlecallSickRatType(Client client, WolfyBot.Core.Packets.Game.actionRequired.callSickRat message)
        { 
            Program.WriteColoredLine($"[{DateTime.Now.ToString("HH:mm:ss")}] Action required : {message.Type} | Timeleft : {message.TimeLeft}.", ConsoleColor.Green, client);
            Task.Factory.StartNew(() => client.InGameIA.ProcessAction(message));
        }

        [MessageAttribute("actionUpdate")]
        public void HandleactionUpdateMessage(Client client, WolfyBot.Core.Packets.Game.NoTypePackets.actionUpdate message)
        {
            Program.WriteColoredLine($"[{DateTime.Now.ToString("HH:mm:ss")}] Action updated : {message.Id} | Timeleft : {message.TimeLeft}.", ConsoleColor.Green, client);
            client.InGameIA.UpdateAction(message);
        }

        [MessageAttribute("actionEnd")]
        public void HandleactionEndMessage(Client client, WolfyBot.Core.Packets.Game.NoTypePackets.actionEnd message)
        {
            Program.WriteColoredLine($"[{DateTime.Now.ToString("HH:mm:ss")}] Action ended : {message.Id}.", ConsoleColor.Green, client);
            client.InGameIA.EndAction(message);
        }
    }
}