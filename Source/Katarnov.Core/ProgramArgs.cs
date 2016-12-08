using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Katarnov
{
    public struct ProgramArgs
    {
        public bool ServerMode;
        public bool Headless;
        public bool LocalHost; 

        public ProgramArgs(
            bool ServerMode = false, 
            bool Headless = false,
            bool LocalHost = false)
        {
            this.ServerMode = ServerMode;
            this.Headless = Headless;
            this.LocalHost = LocalHost;
        }
    }
}
