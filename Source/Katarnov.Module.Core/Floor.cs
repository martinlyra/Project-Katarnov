using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Katarnov.Module.Core
{
    [ByondMapObject(ByondObjectType.Turf, 
        "/turf/simulated/floor",
        "/turf/simulated/floor/plating",
        "/turf/simulated/floor/plating/airless")]
    public class Plating : TurfEntity
    {
        public Plating() : base()
        {
            spritePath = "Content/Turf/plating.png";
        }
    }

    [ByondMapObject(ByondObjectType.Turf, "/turf/simulated/floor/tiled")]
    public class Floor : TurfEntity
    {
        public Floor() : base()
        {
            spritePath = "Content/Turf/steel_dirty.png";
        }
    }
}
