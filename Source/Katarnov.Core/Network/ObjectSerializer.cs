using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace Katarnov.Network
{
    static class ObjectSerializer
    {
        static BinaryFormatter binary = new BinaryFormatter();

        public static byte[] Serialize(object data)
        {
            if (data == null)
                return null;

            using (MemoryStream ms = new MemoryStream())
            {
                binary.Serialize(ms, data);
                return ms.ToArray();
            }
        }

        public static object Deserialize(byte[] data)
        {
            if (data == null)
                return null;

            using (MemoryStream ms = new MemoryStream())
            {
                ms.Write(data, 0, data.Length);
                ms.Seek(0, SeekOrigin.Begin);
                return binary.Deserialize(ms);
            }
        }
        
        public static T Deserialize<T>(byte[] data)
        {
            return (T) Deserialize(data);
        } 
    }
}
