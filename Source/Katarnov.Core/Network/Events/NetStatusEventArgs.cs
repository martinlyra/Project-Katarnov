using Lidgren.Network;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Katarnov.Network.Events
{
    internal class NetStatusEventArgs : EventArgs
    {
        NetIncomingMessage message;

        public NetStatusEventArgs(NetIncomingMessage nim)
        {
            message = nim;
        }

        public NetIncomingMessage Message { get { return message; } }
    }
}
