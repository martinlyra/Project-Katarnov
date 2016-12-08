using Lidgren.Network;
using System;
using System.Collections.Generic;
using System.Linq;
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

        private void ProcessPackets()
        {
            NetIncomingMessage nim;

            while ((nim = ReadMessage()) != null)
            {
                Console.WriteLine(nim.Data);
                switch (nim.MessageType)
                {
                    case NetIncomingMessageType.Data:
                        {
                            RaiseDataRecevied(nim);
                            break;
                        }
                }
            }
        }

        internal override void OnDataReceived(NetIncomingMessage nim)
        {
            var messageType = (NetMessageType) nim.ReadByte();

            base.OnDataReceived(nim);
        }

        private void SendMessage(NetOutgoingMessage nom, NetDeliveryMethod ndm)
        {
            var connections = connectedClients.Select(c => c.NetConnection).ToList();

            netServer.SendMessage(nom, connections, ndm, 0);
        }
    }
}
