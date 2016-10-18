using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Katarnov
{
    [AttributeUsage(AttributeTargets.Class, Inherited = false)]
    public class ByondMapObjectAttribute : Attribute
    {
        private IEnumerable<string> typePaths;
        private ByondObjectType objectType;

        public ByondMapObjectAttribute(ByondObjectType objectType, params string[] typePaths)
        {
            this.objectType = objectType;
            this.typePaths = typePaths;
        }

        public ByondObjectType ObjectType
        {
            get
            {
                return objectType;
            }
        }

        public IEnumerable<string> TypePath
        {
            get
            {
                return typePaths;
            }
        }
    }
}
