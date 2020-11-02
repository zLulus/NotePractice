using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArcGIS3D.WpfDemo.OBBCollisions
{
    /// <summary>
    /// set the relevant elements of our oriented bounding box
    /// </summary>
    public class OBB
    {
        public Vec3 Pos { get; set; }
        public Vec3 AxisX { get; set; }
        public Vec3 AxisY { get; set; }
        public Vec3 AxisZ { get; set; }
        public Vec3 Half_size { get; set; }
    }
}
