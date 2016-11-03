using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Katarnov
{
    public static class Constructor
    {

    }

    public class FauxConstructor<T>
    {
        private Func<T> lambda;
        
        public FauxConstructor(Func<T> lambda)
        {
            this.lambda = lambda;
        }

        public Func<T> Lambda
        {
            get
            {
                return lambda;
            }
        }
    }

    [AttributeUsage(AttributeTargets.Class, Inherited = false)]
    public class SubTypeAttribute : Attribute
    {
        private SubTypeInfo subTypeInfo;

        public SubTypeAttribute(string path, FauxConstructor<Entity> constructor)
        {
            subTypeInfo = new ByondSubTypeInfo(path, "", constructor.Lambda); 
        }
    }
}
