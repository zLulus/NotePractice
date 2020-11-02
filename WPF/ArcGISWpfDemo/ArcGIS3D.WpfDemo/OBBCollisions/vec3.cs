using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArcGIS3D.WpfDemo.OBBCollisions
{
    /// <summary>
    /// define the operations to be used in our 3D vertices
    /// </summary>
    public class Vec3
    {
        public float X { get; set; }
        public float Y { get; set; }
        public float Z { get; set; }
        public Vec3()
        {

        }

        public Vec3(float _x,float _y,float _z)
        {
            X = _x;
            Y = _y;
            Z = _z;
        }

        #region 操作符
        public static Vec3 operator ^(Vec3 num1, Vec3 num2)
        {
            Vec3 v = new Vec3();
            v.X = num1.Y * num2.Z - num1.Z * num2.Y;
            v.Y = num1.Z * num2.X - num1.X * num2.Z;
            v.Z = num1.X * num2.Y - num1.Y * num2.X;
            return v;
        }

        public static Vec3 operator -(Vec3 num1, Vec3 num2)
        {
            Vec3 v = new Vec3();
            v.X = num1.X-num2.X;
            v.Y = num1.Y - num2.Y;
            v.Z = num1.Z - num2.Z;
            return v;
        }

        public static float operator *(Vec3 num1, Vec3 num2)
        {
            return num1.X* num2.X + num1.Y* num2.Y+ num1.Z* num2.Z;
        }

        public static Vec3 operator *(Vec3 num1, float num2)
        {
            Vec3 v = new Vec3();
            v.X = num1.X * num2;
            v.Y = num1.Y * num2;
            v.Z = num1.Z * num2;
            return v;
        }
        #endregion
    }
}
