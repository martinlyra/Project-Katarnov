using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectKatarnov
{
    class ActorObjectCollection : List<ActorObject>
    {
        public new void Add(ActorObject actorObject)
        {
            base.Add(actorObject);
        }
    }
}
