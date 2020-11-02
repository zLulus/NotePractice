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
        public vec3 Pos { get; set; }
        public vec3 AxisX { get; set; }
        public vec3 AxisY { get; set; }
        public vec3 AxisZ { get; set; }
        public vec3 Half_size { get; set; }
    }
}
