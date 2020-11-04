using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WolfyBot.Core.Dispatcher;
using WolfyBot.Core.Packets.Hub.game_create;
using WolfyBot.Core.Packets.Hub.game_update;

namespace WolfyBot.Core.Frames
{
    public class HubFrame
    {
        #region game_update
        [MessageType("PLAYER_COUNT")]// jai fait comme sorte que la methode peux avoir plusieurs attributes 
        public void HandlePLAYER_COUNTType(Client client, PLAYER_COUNT message)
        {
            Program.WriteColoredLine($"[{DateTime.Now.ToString("HH:mm:ss")}] Player(s) left/joined game {message.Id}, new player count : {message.Value}.", ConsoleColor.White);      
        }
        [MessageType("UPDATE_STATUS")]
        public void HandleUPDATE_STATUSType(Client client, UPDATE_STATUS message)
        {
            if(message.Value == 2)
                Program.WriteColoredLine($"[{DateTime.Now.ToString("HH:mm:ss")}] A game [{message.Id}] just closed !",ConsoleColor.Red);
        }
        [MessageType("SLOTS")]
        public void HandleUPDATE_STATUSType(Client client, SLOTS message)
        {
            Program.WriteColoredLine($"[{DateTime.Now.ToString("HH:mm:ss")}] Game : {message.Id} has been updated, slots : {message.Slots}",ConsoleColor.Blue);
        }
        [MessageType("GAME_CREATE")]
        public void HandleGAME_CREATEMessage(Client client, GAME_CREATE message)
        {
            Program.WriteColoredLine($"[{DateTime.Now.ToString("HH:mm:ss")}] A game has been created by [{message.admin.username} -> {message.adminId}].", ConsoleColor.Green);
        }
        #endregion game_update
    }
}
