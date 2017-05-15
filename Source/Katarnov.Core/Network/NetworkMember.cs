using Katarnov.Network.Events;
using Lidgren.Network;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Katarnov.Network
{
    internal abstract class NetworkMember
    {
        protected NetPeer netpeer;
        protected NetPeerConfiguration netPeerConfig;

        internal event EventHandler<ConnectEventArgs> Connected;
        internal event EventHandler<DisconnectEventArgs> Disconnected;
        internal event EventHandler<NetStatusEventArgs> StatusChanged;
        internal event EventHandler<DataReceivedEventArgs> DataReceived;

        public NetworkMember()
        {
            Connected += OnConnected;
            DataReceived += OnDataReceived;
            StatusChanged += OnStatusUpdated;
        }

        protected virtual void OnStatusUpdated(object sender, NetStatusEventArgs e) { }
        protected virtual void OnConnected(object sender, ConnectEventArgs e) { }                                                           
        protected virtual void OnDataReceived(object sender, DataReceivedEventArgs e) { }

        protected virtual void Configure()
        {
            netPeerConfig = new NetPeerConfiguration("Project-K");

            netPeerConfig.EnableMessageType(NetIncomingMessageType.DiscoveryRequest);
            netPeerConfig.EnableMessageType(NetIncomingMessageType.DiscoveryResponse);
            netPeerConfig.EnableMessageType(NetIncomingMessageType.ConnectionApproval);
            netPeerConfig.EnableMessageType(NetIncomingMessageType.StatusChanged);

            netPeerConfig.LocalAddress = NetUtility.Resolve("localhost");
            //netPeerConfig.Port = 2440;
        }

        internal virtual void Connect(string address = "127.0.0.1", int port = 2440)
        {
            
        }

        internal virtual void Disconnect()
        {

        }

        internal virtual void Update()
        {
            
        }

        internal virtual void Draw()
        {

        }

        internal virtual void SendMessage(string message)
        {
            if (netpeer.Connections == null || netpeer.Connections.Count == 0)
                return;
            var outgoingMessage = netpeer.CreateMessage();
            outgoingMessage.Write(message);
            netpeer.SendMessage(outgoingMessage, netpeer.Connections.First(), NetDeliveryMethod.ReliableOrdered);
        }

        internal virtual void SendMessage(NetClientEventMessage eventmessage)
        {
            if (netpeer.Connections == null || netpeer.Connections.Count == 0)
                return;
            var outgoingMessage = netpeer.CreateMessage();

            outgoingMessage.Write((byte)NetMessageType.ClientEvent);

            byte[] data = ObjectSerializer.Serialize(eventmessage);
            int dataSize = data.Length;
            outgoingMessage.Write(dataSize);
            outgoingMessage.Write(data);

            netpeer.SendMessage(outgoingMessage, netpeer.Connections.First(), NetDeliveryMethod.ReliableOrdered);
        }

        internal virtual void SendMessage(object data)
        {
            if (netpeer.Connections == null || netpeer.Connections.Count == 0)
                return;
            var outgoingMessage = netpeer.CreateMessage();
            outgoingMessage.Write(ObjectSerializer.Serialize(data));
            netpeer.SendMessage(outgoingMessage, netpeer.Connections.First(), NetDeliveryMethod.ReliableOrdered);
        }

        internal virtual void OnDataReceived(NetIncomingMessage nim)
        {
            DataReceived.Invoke(this, new DataReceivedEventArgs(nim));
        }

        protected void ProcessPackets()
        {
            NetIncomingMessage nim;

            while ((nim = ReadMessage()) != null)
            {
                //Console.WriteLine(nim.Data);
                switch (nim.MessageType)
                {
                    case NetIncomingMessageType.ConnectionApproval:
                        {
                            RaiseConnection(nim);
                            break;
                        }
                    case NetIncomingMessageType.Data:
                        {
                            RaiseDataRecevied(nim);
                            break;
                        }
                    case NetIncomingMessageType.StatusChanged:
                        {
                            RaiseStatusUpdate(nim);
                            break;
                        }
                }
            }
        }


        protected NetIncomingMessage ReadMessage()
        {
            return netpeer.ReadMessage();
        }

        protected IEnumerable<NetIncomingMessage> ReadMessages()
        {
            var enumerable = new List<NetIncomingMessage>();
            netpeer.ReadMessages(enumerable);
            return enumerable;
        }

        protected void RaiseStatusUpdate(NetIncomingMessage nim)
        {
            StatusChanged.Invoke(this, new NetStatusEventArgs(nim));
        }

        protected void RaiseConnection(NetIncomingMessage nim)
        {
            Connected.Invoke(this, new ConnectEventArgs(nim));
        }

        protected void RaiseDataRecevied(NetIncomingMessage nim)
        {
            DataReceived.Invoke(this, new DataReceivedEventArgs(nim));
        }
    }
}
