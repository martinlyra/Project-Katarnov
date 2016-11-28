using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Katarnov
{
    public class Transform
    {
        double x, y, z;
        double w = 1;

        public Transform()
        {
            x = y = z = 0;
        }

        public Transform(int x, int y, int z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }

        public static Transform Zero()
        {
            return new Transform();
        }

        public void Translate(int x, int y, int z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }

        public void Translate(float x, float y, float z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }

        public void Translate(double x, double y, double z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }

        public Microsoft.Xna.Framework.Vector3 ToXnaVec3()
        {
            return new Microsoft.Xna.Framework.Vector3((float)x, (float)y, (float)z);
        }

        public static explicit operator Microsoft.Xna.Framework.Vector2(Transform t)
        {
            return new Microsoft.Xna.Framework.Vector2((float)t.x, (float)t.y);
        }
        public static explicit operator Microsoft.Xna.Framework.Vector3(Transform t)
        {
            return new Microsoft.Xna.Framework.Vector3((float)t.x, (float)t.y, (float)t.z);
        }

        public double X { get { return x; } set { x = value; } }
        public double Y { get { return y; } set { y = value; } }
        public double Z { get { return z; } set { z = value; } }
        public double W { get { return w; } set { w = value; } }

        public float Xf { get { return (float)x; } }
        public float Yf { get { return (float)y; } }
        public float Zf { get { return (float)z; } }
        public float Wf { get { return (float)w; } }
    }
}
