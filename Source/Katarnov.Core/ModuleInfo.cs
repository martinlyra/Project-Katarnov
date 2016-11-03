using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Katarnov.Module
{
    public class ModuleInfo
    {
        private IModule module  = null;
        private bool activated  = false;
        private string path     = "";

        internal static ModuleInfo CreateFrom(Type moduleType)
        {
            var modInfo = new ModuleInfo();
            modInfo.module = moduleType.InstantizeAs<IModule>();
            return modInfo;
        }

        public bool IsActivated()
        {
            return activated;
        }

        public IModule Interface
        {
            get
            {
                return module;
            }
        }
    }
}
