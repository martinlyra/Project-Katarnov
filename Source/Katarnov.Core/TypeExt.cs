using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Katarnov
{
    public static class TypeExt
    {
        public static bool Implements(this Type type, Type interfaceType)
        {
            return type.GetInterface(interfaceType.Name) != null;   
        }

        public static bool Implements<T>(this Type type)
        {
            return type.Implements(typeof(T));
        }

        public static bool Inherits(this Type type, Type targetBaseType)
        {
            var baseType = type.BaseType;

            while (baseType != null)
            {
                if (baseType.Equals(targetBaseType))
                    return true;
                baseType = baseType.BaseType;
            }

            return false;
        }

        public static bool Inherits<T>(this Type type)
        {
            return Inherits(type, typeof(T));
        }

        public static IEnumerable<Type> GetInheritedBaseTypes(this Type type)
        {
            var baseType = type.BaseType;
            var returnList = new List<Type>();

            while (baseType != null)
            {
                returnList.Add(baseType);
                baseType = baseType.BaseType;
            }

            return returnList;
        }

        public static T InstantizeAs<T>(this Type type)
        {
            try {
                return (T)Activator.CreateInstance(type);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return default(T);
            }
        }

        public static T TryGetAttribute<T>(this Type type) where T : Attribute
        {
            try
            {
                return (T)Attribute.GetCustomAttribute(type, typeof(T));
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }
        }

        public static bool HasAttribute<T>(this Type type) where T : Attribute
        {
            return type.TryGetAttribute<T>() != null;
        }

        public static bool HasMethod(this Type type, MethodInfo method)
        {
            return type.GetMethod(method.Name) != null;
        }

        public static bool HasMethod(this Type type, Action method)
        {
            return type.HasMethod(method.Method);
        }
    }
}
