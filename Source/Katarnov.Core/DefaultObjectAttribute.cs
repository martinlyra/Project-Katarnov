using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Katarnov
{

    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public class DefaultObjectAttribute : Attribute
    {
        private ByondObjectType objectType;

        public DefaultObjectAttribute(ByondObjectType objectType)
        {

        }
    }
}
