using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Katarnov
{
    public static class Input
    {
        private static KeyboardState keyboard;

        internal static void Update()
        {
            keyboard = Keyboard.GetState();
        }
    }
}
