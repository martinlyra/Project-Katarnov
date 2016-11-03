using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Katarnov.Module.Core.Object
{
    [ByondMapObject(ByondObjectType.Object, "/obj/structure/grille")]
    public class Grille : ObjectEntity
    {
        public Grille() : base()
        {
            spritePath = "Content/Object/grille.png";
        }
    }
}
