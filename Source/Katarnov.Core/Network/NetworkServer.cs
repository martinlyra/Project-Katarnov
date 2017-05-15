using Katarnov.Input;
using Katarnov.Network.Events;
using Lidgren.Network;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Katarnov.Network
{
    class NetworkServer : NetworkMember
    {
        protected List<Client> connectedClients;
        protected List<Client> disconnectedClients;

        private NetServer netServer;

        public NetworkServer()
        {
            Configure();
            Start();
        }

        private void Start()
        {
            try
            {
                netPeerConfig.Port = 2440;
                netServer = new NetServer(netPeerConfig);
                netpeer = netServer as NetPeer;
                netServer.Start();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        internal override void Update()
        {
            NetworkUpdate();
        }

        private void NetworkUpdate()
        {
            /*
            foreach (Client client in connectedClients)
            {
                client.NetChannel.Update();
            }
            */
            ProcessPackets(); 
        }

        protected override void OnConnected(object sender, ConnectEventArgs e)
        {
            var nim = e.Message;
            var client = nim.SenderConnection.Peer.Configuration.BroadcastAddress;

            nim.SenderConnection.Approve();
            Console.WriteLine($"{client} has connected to server.");
        }

        protected override void OnDataReceived(object sender, DataReceivedEventArgs e)
        {
            var nim = e.Message;
            var client = nim.SenderConnection.Peer.Configuration.BroadcastAddress;
            nim.Position = 0;
            var messageType = (NetMessageType)nim.ReadByte();

            switch(messageType)
            {
                case NetMessageType.ClientEvent:
                    {
                        var dataSize = nim.ReadInt32();
                        var clientEvent = ObjectSerializer.Deserialize<NetClientEventMessage>(
                            nim.ReadBytes(dataSize));

                        if (clientEvent.EventType == NetClientEventType.CommandState)
                        {
                            Console.Write($"{client.ToString()} has pressed: ");
                            KeyCommand[] cmds = (KeyCommand[])clientEvent.Data;
                            foreach (var cmd in cmds)
                                Console.Write(cmd);
                            Console.Write("\n");
                        }

                        break;
                    }
            }
        }

        private void SendMessage(NetOutgoingMessage nom, NetDeliveryMethod ndm)
        {
            var connections = connectedClients.Select(c => c.NetConnection).ToList();

            netServer.SendMessage(nom, connections, ndm, 0);
        }
    }
}
