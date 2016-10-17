using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Katarnov
{
    public class EntityIdentifier
    {
        private uint _uid       = 0;
        private bool assigned   = false;

        private Entity _associatedEntity;

        public bool IsAssigned()
        {
            return assigned;
        }

        public Entity GetAssociatedEntity()
        {
            return !assigned ? null : _associatedEntity;
        }

        public void Assign(uint newId, Entity e)
        {
            if (assigned)
                return;

            _uid = newId;
            _associatedEntity = e;

            assigned = true;
        }

        public uint GetId()
        {
            return _uid;
        }
    }
}
