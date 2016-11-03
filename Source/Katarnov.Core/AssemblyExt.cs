using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Katarnov
{
    public static class AssemblyExt
    {
        public static IEnumerable<TypeInfo> GetImplementationsOf<T>(this Assembly ass)
        {
            return ass.DefinedTypes.Where(t => t.Implements<T>());
        }

        public static bool ImportsType<T>(this Assembly ass) // do not judge me for this
        {
            foreach (var t in ass.DefinedTypes)
                if (t.Inherits<T>())
                    return true;
            return false;
        }

        public static bool ExposesTypeOf<T>(this Assembly ass)
        {
            foreach (var t in ass.ExportedTypes)
                if (t.Inherits<T>())
                    return true;
            return false;
        }
    }
}
