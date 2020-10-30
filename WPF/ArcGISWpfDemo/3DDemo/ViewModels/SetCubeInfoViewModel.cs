using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArcGIS3D.WpfDemo.ViewModels
{
    public class SetCubeInfoViewModel: NotifyPropertyChanged
    {
        private double x;
        public double X
        {
            get
            {
                return x;
            }
            set
            {
                x = value;
                RaisePropertyChanged(nameof(X));
            }
        }
        private double y;
        public double Y
        {
            get
            {
                return y;
            }
            set
            {
                y = value;
                RaisePropertyChanged(nameof(Y));
            }
        }
        private double z;
        public double Z
        {
            get
            {
                return z;
            }
            set
            {
                z = value;
                RaisePropertyChanged(nameof(Z));
            }
        }
        private double width;
        /// <summary>
        /// x
        /// </summary>
        public double Width
        {
            get
            {
                return width;
            }
            set
            {
                width = value;
                RaisePropertyChanged(nameof(Width));
            }
        }
        private double height;
        /// <summary>
        /// z
        /// </summary>
        public double Height
        {
            get
            {
                return height;
            }
            set
            {
                height = value;
                RaisePropertyChanged(nameof(Height));
            }
        }
        private double depth;
        /// <summary>
        /// y
        /// </summary>
        public double Depth
        {
            get
            {
                return depth;
            }
            set
            {
                depth = value;
                RaisePropertyChanged(nameof(Depth));
            }
        }
        private double heading;
        /// <summary>
        /// 角度
        /// </summary>
        public double Heading
        {
            get
            {
                return heading;
            }
            set
            {
                heading = value;
                RaisePropertyChanged(nameof(Heading));
            }
        }
    }
}
