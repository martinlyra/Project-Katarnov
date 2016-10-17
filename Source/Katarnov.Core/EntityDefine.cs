using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Katarnov
{
    public class EntityDefine
    {
        public string typeName;
        public string spriteDef;

        public EntityDefine()
        {
            typeName = GetType().Name;
            spriteDef = null;
        }
    }
}
