using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectKatarnov
{
    class Map
    {
        public Vector2 size;
        public List<Level> levels = new List<Level>();

        public ActorObjectCollection actorObjects = new ActorObjectCollection();
    }
}
