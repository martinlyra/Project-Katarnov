using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Katarnov
{
    [AttributeUsage(AttributeTargets.Class, Inherited = false)]
    public class SubTypesAttribute : Attribute
    {
        private IDictionary<string, Func<Entity>> lambdaConstructors;

        public SubTypesAttribute(string path, Func<Entity> constructor)
        {
            lambdaConstructors = new Dictionary<string, Func<Entity>>();
            lambdaConstructors.Add(path, constructor);
        }

        public SubTypesAttribute(KeyValuePair<string, Func<Entity>>[] dictionary)
        {
        
        }

        public SubTypesAttribute(IDictionary<string,Func<Entity>> dictionary)
        {
            lambdaConstructors = dictionary;
        }

        public IEnumerable<string> SubTypePaths
        {
            get
            {
                return lambdaConstructors.Keys;
            }
        }

        public IEnumerable<Func<Entity>> LambdaConstructors
        {
            get
            {
                return lambdaConstructors.Values;
            }
        }

        public IDictionary<string, Func<Entity>> SubTypes
        {
            get
            {
                return lambdaConstructors;
            }
        }
    }
}
