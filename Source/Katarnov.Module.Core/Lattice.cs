using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Katarnov.Module.Core
{
    [ByondMapObject(ByondObjectType.Object,"/obj/structure/lattice")]
    public class Lattice : ObjectEntity
    {
        public Lattice() : base()
        {
            spritePath = "Content/Object/lattice.png";
        }
    }
}
