using Lidgren.Network;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Katarnov.Network.Events
{
    class DataReceivedEventArgs : EventArgs
    {
        private readonly NetIncomingMessage message;

        public DataReceivedEventArgs(NetIncomingMessage nim)
        {
            message = nim;
        }

        public NetIncomingMessage Message
        {
            get
            {
                return message;
            }
        }
    }
}
