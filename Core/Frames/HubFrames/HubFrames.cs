using System;
using System.Linq;
using WolfyBot.Core.Dispatcher;
using WolfyBot.Core.Game;
using WolfyBot.Core.Game.Types;
using WolfyBot.Core.Packets.Hub.game_update;
using WolfyBot.Core.Packets.Hub.NoTypePackets;

namespace WolfyBot.Core.Frames.HubFrames
{
    public class HubFrames
    {
        #region game_update

        [MessageAttribute("game_update", "PLAYER_COUNT")]// jai fait comme sorte que la methode peux avoir plusieurs attributes
        public void HandlePLAYER_COUNTType(Client client, PLAYER_COUNT message)
        {
            Program.WriteColoredLine($"[{DateTime.Now.ToString("HH:mm:ss")}] Player(s) left/joined game {message.Id}, new player count : {message.Value}.", ConsoleColor.White, client);
        }

        [MessageAttribute("game_update", "UPDATE_STATUS")]
        public void HandleUPDATE_STATUSType(Client client, UPDATE_STATUS message)
        {
            if (message.Value == 2) // 1 = attente de joueurs 2 = décompte avant départ
                Program.WriteColoredLine($"[{DateTime.Now.ToString("HH:mm:ss")}] A game [{message.Id}] just closed !", ConsoleColor.Yellow, client);
        }

        [MessageAttribute("game_update", "SLOTS")]
        public void HandleUPDATE_STATUSType(Client client, SLOTS message)
        {
            Program.WriteColoredLine($"[{DateTime.Now.ToString("HH:mm:ss")}] Game : {message.Id} has been updated, slots : {message.Slots}", ConsoleColor.Blue, client);
        } 
        
        [MessageAttribute("hydrate")]
        public void HandlehydrateMessage(Client client, hydrate message)
        {
            Program.WriteColoredLine($"[{DateTime.Now.ToString("HH:mm:ss")}] Got hydrade message !", ConsoleColor.Blue, client);
        }

        [MessageAttribute("game_create")]
        public void Handlegame_createMessage(Client client, game_create message)
        {
            Program.WriteColoredLine($"[{DateTime.Now.ToString("HH:mm:ss")}] A game has been created by [{message.admin.username} -> {message.adminId}].", ConsoleColor.Green, client);
            if (Program.AreClientsConnectingToGame && !message.voice)
            {
                Program.WriteColoredLine($"[{DateTime.Now.ToString("HH:mm:ss")}] A game has been created, joining it ! GameID : {message.id}, GameInstanceID : {message.instanceId}", ConsoleColor.Cyan, client);                
                client.Quit();
                client.ConnectToWorld(message.id, message.instanceId);
            }
        }

        #endregion game_update
    }
}