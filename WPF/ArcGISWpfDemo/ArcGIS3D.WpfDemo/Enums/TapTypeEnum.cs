using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArcGIS3D.WpfDemo.Enums
{
    public enum TapTypeEnum
    {
        None=0,
        /// <summary>
        /// 移动观察者
        /// </summary>
        MoveViewPoint = 1,
        /// <summary>
        /// 绘制立方体-选择多点
        /// </summary>
        DrawByPolygon = 2,
        /// <summary>
        /// 绘制立方体-选择中心点
        /// </summary>
        DrawByCenter = 3,
        /// <summary>
        /// 高亮选中
        /// </summary>
        Select=4,
    }
}
