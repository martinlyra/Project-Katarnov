using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Katarnov.Module.Core.Turf
{

    [ByondMapObject(ByondObjectType.Turf,
        "/turf/simulated/floor")]
    public class Floor : TurfEntity
    {
        Flooring flooring;

        public Floor() : base()
        {
            spritePath = "Content/Turf/plating.png";
        }

        public override void PostConstruct(EntityInstanceArgs args)
        {
            base.PostConstruct(args);

            ByondObjectInstanceArgs bargs = (ByondObjectInstanceArgs)args;
            string ext = bargs.TypePath.Extension;

            switch (ext)
            {
                case ("/reinforced"):
                    {
                        spritePath = "Content/Turf/floor_reinforced.png";
                        break;
                    }
                case ("/tiled"):
                    {
                        spritePath = "Content/Turf/floor_steel.png";
                        break;
                    }
                case ("/tiled/dark"):
                    {
                        spritePath = "Content/Turf/floor_dark.png";
                        break;
                    }
                case ("/tiled/white"):
                    {
                        spritePath = "Content/Turf/floor_white.png";
                        break;
                    }
                case ("/tiled/freezer"):
                    {
                        spritePath = "Content/Turf/floor_plastic.png";
                        break;
                    }
                case ("/carpet"):
                    {
                        spritePath = "Content/Turf/floor_carpet.png";
                        break;
                    }
                case ("/carpet/blue"):
                    {
                        spritePath = "Content/Turf/floor_carpet_blue.png";
                        break;
                    }
                case ("/lino"):
                    {
                        spritePath = "Content/Turf/floor_linoleum.png";
                        break;
                    }
                case ("/wood"):
                    {
                        spritePath = "Content/Turf/floor_wood.png";
                        break;
                    }
            }   
        }
    }
    
    /*
    [ByondMapObject(ByondObjectType.Turf, 
        "/turf/simulated/floor/tiled",
        "/turf/simulared/floor/tiled/airless")]
    public class Floor : TurfEntity
    {
        public Floor() : base()
        {
            spritePath = "Content/Turf/floor_steel.png";
        }
    }

    [ByondMapObject(ByondObjectType.Turf,
        "/turf/simulated/floor/tiled/steel")]
    public class SteelFloor : TurfEntity
    {
        public SteelFloor() : base()
        {
            spritePath = "Content/Turf/steel_dirty.png";
        }
    }

    [ByondMapObject(ByondObjectType.Turf,
        "/turf/simulated/floor/wood")]
    public class WoodenFloor : TurfEntity
    {
        public WoodenFloor() : base()
        {
            spritePath = "Content/Turf/floor_wood.png";
        }
    }

    [ByondMapObject(ByondObjectType.Turf,
        "/turf/simulated/floor/tiled/dark")]
    public class DarkFloor : TurfEntity
    {
        public DarkFloor() : base()
        {
            spritePath = "Content/Turf/floor_dark.png";
        }
    }

    [ByondMapObject(ByondObjectType.Turf,
        "/turf/simulated/floor/tiled/white")]
    public class WhiteFloor : TurfEntity
    {
        public WhiteFloor() : base()
        {
            spritePath = "Content/Turf/floor_white.png";
        }
    }

    [ByondMapObject(ByondObjectType.Turf,
        "/turf/simulated/floor/tiled/freezer")]
    public class PlasticFloor : TurfEntity
    {
        public PlasticFloor() : base()
        {
            spritePath = "Content/Turf/floor_plastic.png";
        }
    }

    [ByondMapObject(ByondObjectType.Turf,
        "/turf/simulated/floor/lino")]
    public class LinoleumFloor : TurfEntity
    {
        public LinoleumFloor() : base()
        {
            spritePath = "Content/Turf/floor_linoleum.png";
        }
    }

    [ByondMapObject(ByondObjectType.Turf,
        "/turf/simulated/floor/carpet")]
    public class Carpet : TurfEntity
    {
        public Carpet() : base()
        {
            spritePath = "Content/Turf/floor_carpet.png";
        }
    }

    [ByondMapObject(ByondObjectType.Turf, 
        "/turf/simulated/floor/reinforced",
        "/turf/simulated/floor/reinforced/airless")]
    public class FloorReinforced : TurfEntity
    {
        public FloorReinforced() : base()
        {
            spritePath = "Content/Turf/floor_reinforced.png";
        }
    }
    */
}
