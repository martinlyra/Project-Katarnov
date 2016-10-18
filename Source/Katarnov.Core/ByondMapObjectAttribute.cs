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
        private string typePath;
        private ByondObjectType objectType;

        public ByondMapObjectAttribute(ByondObjectType objectType, string typePath)
        {
            this.objectType = objectType;
            this.typePath = typePath;
        }

        public ByondObjectType ObjectType
        {
            get
            {
                return objectType;
            }
        }

        public string TypePath
        {
            get
            {
                return typePath;
            }
        }
    }
}
