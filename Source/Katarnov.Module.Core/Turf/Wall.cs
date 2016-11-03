using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Katarnov;

namespace Katarnov.Module.Core.Turf
{
    /*public class Wall : EntityDefine
    {
        public Wall() : base()
        {
            spriteDef = "Content/Turf/wall.png";
        }
    }*/

    [ByondMapObject(ByondObjectType.Turf,"/turf/simulated/wall")]
    public class Wall : TurfEntity
    {
        public Wall () : base()
        {
            spritePath = "Content/Turf/wall.png";
        }
    }

    [ByondMapObject(ByondObjectType.Turf, "/turf/simulated/wall/r_wall")]
    public class WallReinforced : Wall
    {
        public WallReinforced() : base()
        {
            spritePath = "Content/Turf/wall_reinforced.png";
        }
    }
}
