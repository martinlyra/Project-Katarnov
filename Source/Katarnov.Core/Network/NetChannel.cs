using Lidgren.Network;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Katarnov.Network
{
    internal class NetChannel
    {
        private NetSender sender;
        private NetReceiver receiver;

        public NetChannel(NetPeer host)
        {
            sender = new NetSender();
            receiver = new NetReceiver();
        }

        internal void Update()
        {

        }
    }
}
