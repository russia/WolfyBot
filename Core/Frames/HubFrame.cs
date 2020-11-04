using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WolfyBot.Core.Dispatcher;
using WolfyBot.Core.Packets.Hub.game_update;

namespace WolfyBot.Core.Frames
{
    public class HubFrame
    {
        #region game_update
        [MessageType("PLAYER_COUNT")]// jai fait comme sorte que la methode peux avoir plusieurs attributes 
        public void HandlePLAYER_COUNTType(Client client, PLAYER_COUNT message)
        {
            //TODO
            Console.WriteLine($"Player count : {message.Value}, gameid {message.Id}");      
        }
        [MessageType("UPDATE_STATUS")]// jai fait comme sorte que la methode peux avoir plusieurs attributes 
        public void HandleUPDATE_STATUSType(Client client, UPDATE_STATUS message)
        {
            //TODO
            Console.WriteLine("Update Status !");
        }
        #endregion game_update
    }
}
