using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Katarnov;

namespace Katarnov.Module.Core
{
    /*public class Wall : EntityDefine
    {
        public Wall() : base()
        {
            spriteDef = "Content/Turf/wall.png";
        }
    }*/

    public class WallEntity : Entity
    {
        public new string spritePath = "Content/Turf/wall.png";
    }
}
