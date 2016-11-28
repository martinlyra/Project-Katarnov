using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Katarnov
{
    public class ByondObjectInstanceArgs : EntityInstanceArgs
    {
        ByondTypePath _typePath;
        string _uniqueName = "";
        Dictionary<string, string> _additionalArgs = new Dictionary<string, string>();

        public ByondObjectInstanceArgs(ByondTypePath path, Dictionary<string,string> args = null)
        {
            _typePath = path;
            _additionalArgs = args;
        }

        public ByondTypePath TypePath
        {
            get
            {
                return _typePath;
            }
        }

        // not supported right now
        public string UniqueName
        {
            get
            {
                return _uniqueName;
            }
        }

        public Dictionary<string, string> AdditionalArgs
        {
            get
            {
                return _additionalArgs;
            }
        }
    }
}
