using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectKatarnov
{
    class Transform
    {
        double x, y, z, w;

        public static explicit operator Microsoft.Xna.Framework.Vector2(Transform t)
        {
            return new Microsoft.Xna.Framework.Vector2((float)t.x, (float)t.y);
        }
        public static explicit operator Microsoft.Xna.Framework.Vector3(Transform t)
        {
            return new Microsoft.Xna.Framework.Vector3((float)t.x, (float)t.y, (float)t.z);
        }
    }
}
