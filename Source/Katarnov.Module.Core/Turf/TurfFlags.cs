using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Katarnov.Module.Core.Turf
{
    [Flags]
    enum TurfFlags
    {
        RemoveableCrowbar = 1,
        RemoveableScrewdriver = 2,
        RemoveableWrench = 4,
        RemoveableShovel = 8,
        CanBreak = 16,
        CanBurn = 32,
        HasEdges = 64,
        HasCorners = 128,
        IsFragile = 256,
        AcidImmune = 512
    }
}
