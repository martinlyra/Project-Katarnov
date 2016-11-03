using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Katarnov.Module.Core.Object
{
    [ByondMapObject(ByondObjectType.Object, "/obj/structure/table/standard")]
    public class Table : ObjectEntity
    {
        public Table() : base()
        {
            spritePath = "Content/Object/table.png";
        }
    }
}
