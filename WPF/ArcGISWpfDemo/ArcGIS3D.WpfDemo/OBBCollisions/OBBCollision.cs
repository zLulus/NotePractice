using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArcGIS3D.WpfDemo.OBBCollisions
{

    public class OBBCollision
    {
        /// <summary>
        /// check if there's a separating plane in between the selected axes
        /// </summary>
        /// <returns></returns>
        public static bool GetSeparatingPlane( Vec3 RPos,  Vec3 Plane,  OBB box1,  OBB box2)
        {
            return (Math.Abs(RPos* Plane) > 
                (Math.Abs((box1.AxisX* box1.Half_size.X)*Plane) +
                Math.Abs((box1.AxisY* box1.Half_size.Y)*Plane) +
                Math.Abs((box1.AxisZ* box1.Half_size.Z)*Plane) +
                Math.Abs((box2.AxisX* box2.Half_size.X)*Plane) +
                Math.Abs((box2.AxisY* box2.Half_size.Y)*Plane) +
                Math.Abs((box2.AxisZ* box2.Half_size.Z)*Plane)));
        }

        /// <summary>
        /// test for separating planes in all 15 axes
        /// </summary>
        /// <param name="box1"></param>
        /// <param name="box2"></param>
        /// <returns></returns>
        public static bool GetCollision(OBB  box1,  OBB box2)
        {
            Vec3 RPos;
            RPos = box2.Pos - box1.Pos;

            return !(GetSeparatingPlane(RPos, box1.AxisX, box1, box2) ||
                GetSeparatingPlane(RPos, box1.AxisY, box1, box2) ||
                GetSeparatingPlane(RPos, box1.AxisZ, box1, box2) ||
                GetSeparatingPlane(RPos, box2.AxisX, box1, box2) ||
                GetSeparatingPlane(RPos, box2.AxisY, box1, box2) ||
                GetSeparatingPlane(RPos, box2.AxisZ, box1, box2) ||
                GetSeparatingPlane(RPos, box1.AxisX ^ box2.AxisX, box1, box2) ||
                GetSeparatingPlane(RPos, box1.AxisX ^ box2.AxisY, box1, box2) ||
                GetSeparatingPlane(RPos, box1.AxisX ^ box2.AxisZ, box1, box2) ||
                GetSeparatingPlane(RPos, box1.AxisY ^ box2.AxisX, box1, box2) ||
                GetSeparatingPlane(RPos, box1.AxisY ^ box2.AxisY, box1, box2) ||
                GetSeparatingPlane(RPos, box1.AxisY ^ box2.AxisZ, box1, box2) ||
                GetSeparatingPlane(RPos, box1.AxisZ ^ box2.AxisX, box1, box2) ||
                GetSeparatingPlane(RPos, box1.AxisZ ^ box2.AxisY, box1, box2) ||
                GetSeparatingPlane(RPos, box1.AxisZ ^ box2.AxisZ, box1, box2));
        }
    }
}
