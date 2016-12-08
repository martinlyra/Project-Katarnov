using Lidgren.Network;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Katarnov.Network
{
    internal class Client
    {
        private NetChannel netChannel;
        private NetConnection netConnection;

        internal NetConnection NetConnection
        {
            get
            {
                return netConnection;
            }
        }

        internal NetChannel NetChannel
        {
            get
            {
                return netChannel;
            }
        }
    }
}
