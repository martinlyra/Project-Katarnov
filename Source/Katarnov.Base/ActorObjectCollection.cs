using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Katarnov
{
    class EntityCollection : List<Entity>
    {
        public new void Add(Entity actorObject)
        {
            base.Add(actorObject);
        }
    }
}
