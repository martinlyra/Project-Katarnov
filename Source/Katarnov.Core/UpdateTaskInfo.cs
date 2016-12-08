using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Katarnov
{
    enum UpdateQueryTrigger
    {
        DefaultQuery,
        EventQuery,
        ReactionTrigger
    }

    internal struct UpdateTaskInfo
    {
        public IUpdatable Object;
        
        public UpdateTaskInfo(IUpdatable targetObject)
        {
            Object = targetObject;
        }    
    }
}
