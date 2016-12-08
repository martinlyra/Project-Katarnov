using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Katarnov.Network
{
    /*[Flags]
    enum NetMessageFlags
    {
        Ping = 1,
        Request = 2,

    }*/

    enum NetMessageType
    {
        Unknown = 0,
        Ping = 1, 
        Pong = 2, // ping answer

        ClientName,
        ServerName,
        MaxPlayers,
        PlayerCount,

        FetchState,

        StateRequest,
        StateMessage,
    }
}
