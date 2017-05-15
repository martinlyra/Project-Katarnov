using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Katarnov.Input
{
    class KeybindConfiguration
    {
        #region Default Keybinds
        static readonly Dictionary<KeyCommand, Keys[]> defaultKeybindingDictionary =
            new Dictionary<KeyCommand, Keys[]>
            {
                { KeyCommand.MoveNorth, new Keys[] { Keys.W, Keys.Up } },
                { KeyCommand.MoveSouth, new Keys[] { Keys.S, Keys.Down } },
                { KeyCommand.MoveWest, new Keys[] { Keys.A, Keys.Left } },
                { KeyCommand.MoveEast, new Keys[] { Keys.D, Keys.Right } }
            };
        #endregion

        Dictionary<Keys, KeyCommand> keybindingDictionary;

        public KeybindConfiguration()
        {
            keybindingDictionary = new Dictionary<Keys, KeyCommand>();
        }

        public void MakeDefaultBingdings(bool resetAll)
        {
            if (resetAll)
                keybindingDictionary.Clear();

            //TODO: Remove already defined binds from copy
            foreach (var kp in defaultKeybindingDictionary)
            {
                var command = kp.Key;
                var keys = kp.Value;

                if (keys.Length < 1)
                    continue;

                for (int i = 0; i < keys.Length; i++)
                {
                    keybindingDictionary.Add(keys[i], command);
                }
            }
        }

        public KeyCommand GetCommand(Keys key)
        {
            return keybindingDictionary[key];
        }

        public List<KeyCommand> GetCommands(Keys[] keys)
        {
            var commands = new List<KeyCommand>();

            foreach (var key in keys)
                commands.Add(GetCommand(key));

            return commands;
        }
    }
}
