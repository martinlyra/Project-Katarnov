using Katarnov.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Katarnov
{
    public static class World
    {
        public static event EventHandler<WorldInitializeEventArgs> OnInitialize;
        public static event EventHandler<WorldReadyEventArgs> OnReady;

        public static void Ready()
        {
            OnReady.Invoke(null, new WorldReadyEventArgs());
        }
    }
}
