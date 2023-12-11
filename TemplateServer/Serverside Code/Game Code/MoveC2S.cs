using PlayerIO.GameLibrary;
using SimServer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimAssemblyExample
{
    internal class MoveC2S : IFunction
    {
        public void Execute(Player player, Message message, GameCode game)
        {
            int oldPosX = message.GetInt(0);
            int oldPosY = message.GetInt(1);
            int newPosX = message.GetInt(2);
            int newPosY = message.GetInt(3);
            Console.WriteLine($"old {oldPosX}:{oldPosY} - new {newPosX}:{newPosY}");

            game.Broadcast("MOVE", player.ConnectUserId, oldPosX, oldPosY, newPosX, newPosY);
        }
    }
}
