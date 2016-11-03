using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Katarnov.Module
{
    public interface IModule
    {
        string Name { get; }
        string Description { get; }
        string Version { get; }

        Entity SpawnDefault();
    }
}
