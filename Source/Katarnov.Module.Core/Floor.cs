using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Katarnov.Module.Core
{
    [ByondMapObject(ByondObjectType.Turf, "/turf/simulated/floor")]
    public class Plating : TurfEntity
    {
        public Plating() : base()
        {
            spritePath = "Content/Turf/plating.png";
        }

        public static IEnumerable<SubTypeInfo> SubTypes
        {
            get
            {
                return subTypes;
            }
        }

        private static IEnumerable<SubTypeInfo> subTypes = new List<SubTypeInfo>()
                {
                    new ByondSubTypeInfo(
                        "/turf/simulated/floor/plating",
                        "",
                        () =>
                        {
                            return new Plating();
                        }),
                    new ByondSubTypeInfo(
                        "/turf/simulated/floor/airless",
                        "",
                        () =>
                        {
                            return new Plating();
                        }
                        )
                };
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
