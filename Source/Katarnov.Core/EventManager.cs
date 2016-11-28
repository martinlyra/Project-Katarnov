using Katarnov.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Katarnov
{
    public static class EventManager
    {
        public static event EventHandler<WorldInitializeEventArgs> OnWorldInitialize;
        public static event EventHandler<WorldReadyEventArgs> OnWorldReady;
    }
}
