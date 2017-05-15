using OpenTK.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Katarnov.Network
{
    [Serializable]
    enum NetClientEventType
    {
        CommandState,
        MouseState
    }

    [Serializable]
    class NetClientEventMessage
    {
        public NetClientEventType EventType;
        public object Data;

        public NetClientEventMessage(NetClientEventType type, object data)
        {
            EventType = type;
            Data = data;
        }
    }
}
