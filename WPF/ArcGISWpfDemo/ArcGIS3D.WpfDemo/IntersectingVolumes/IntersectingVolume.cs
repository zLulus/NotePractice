using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArcGIS3D.WpfDemo.IntersectingVolumes
{
    public class IntersectingVolume
    {
        public static float CalculateIntersectingVolume(Cuboid A, Cuboid B)
        {
            return Math.Max(Math.Min(A.RightTopPoint.X, B.RightTopPoint.X) - Math.Max(A.LeftBottomPoint.X, B.LeftBottomPoint.X), 0)
                    * Math.Max(Math.Min(A.RightTopPoint.Y,B.RightTopPoint.Y) - Math.Max(A.LeftBottomPoint.Y, B.LeftBottomPoint.Y), 0)
                    * Math.Max(Math.Min(A.RightTopPoint.Z,B.RightTopPoint.Z) - Math.Max(A.LeftBottomPoint.Z, B.LeftBottomPoint.Z), 0);
        }
    }
}
