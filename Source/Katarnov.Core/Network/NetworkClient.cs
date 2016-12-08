﻿using Lidgren.Network;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Katarnov.Network
{
    internal class NetworkClient : NetworkMember
    {
        private NetClient netClient;

        public NetworkClient()
        {
            Configure();
            Start();
        }

        private void Start()
        {
            try
            {
                netClient = new NetClient(netPeerConfig);
                netpeer = netClient as NetPeer;
                netClient.Start();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        internal override void Connect(string address = "127.0.0.1", int port = 2440)
        {
            netpeer.Connect(address, port);
        }
    }
}
