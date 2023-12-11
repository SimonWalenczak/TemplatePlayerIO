using PlayerIO.GameLibrary;
using SimServer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimAssemblyExample
{
    internal interface IFunction
    {
        void Execute(Player player, Message message, GameCode game);
    }
}
