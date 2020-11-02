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
    public class vec3
    {
        public float x { get; set; }
        public float y { get; set; }
        public float z { get; set; }
        public vec3()
        {

        }

        public vec3(float _x,float _y,float _z)
        {
            x = _x;
            y = _y;
            z = _z;
        }

        public static vec3 operator ^(vec3 num1, vec3 num2)
        {
            vec3 v = new vec3();
            v.x = num1.y * num2.z - num1.z * num2.y;
            v.y = num1.z * num2.x - num1.x * num2.z;
            v.z = num1.x * num2.y - num1.y * num2.x;
            return v;
        }

        public static vec3 operator -(vec3 num1, vec3 num2)
        {
            vec3 v = new vec3();
            v.x = num1.x-num2.x;
            v.y = num1.y - num2.y;
            v.z = num1.z - num2.z;
            return v;
        }

        public static float operator *(vec3 num1, vec3 num2)
        {
            return num1.x* num2.x + num1.y* num2.y+ num1.z* num2.z;
        }

        public static vec3 operator *(vec3 num1, float num2)
        {
            vec3 v = new vec3();
            v.x = num1.x * num2;
            v.y = num1.y * num2;
            v.z = num1.z * num2;
            return v;
        }
    }
}
