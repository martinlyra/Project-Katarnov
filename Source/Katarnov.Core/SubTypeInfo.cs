using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Katarnov
{
    public class ByondSubTypeInfo : SubTypeInfo
    {
        readonly string byondSubTypePath;

        public ByondSubTypeInfo(string subTypePath, string name, Func<Entity> lambdaConstructor)
            : base(name, lambdaConstructor)
        {
            byondSubTypePath = subTypePath;
        }

        public string ByondSubTypePath
        {
            get
            {
                return byondSubTypePath;
            }
        }
    }

    public class SubTypeInfo
    {
        readonly string subTypeName;
        readonly Func<Entity> lambdaConstructor;

        public Func<Entity> LambdaConstructor
        {
            get
            {
                return lambdaConstructor;
            }
        }

        public string SubTypeName
        {
            get
            {
                return subTypeName;
            }
        }

        public SubTypeInfo(string name, Func<Entity> lambdaConstructor)
        {
            subTypeName = name;
            this.lambdaConstructor = lambdaConstructor;
        }
    }
}
