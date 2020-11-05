using ArcGIS3D.WpfDemo.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ArcGIS3D.WpfDemo.Dialogs
{
    /// <summary>
    /// Interaction logic for SetCubeInfo.xaml
    /// </summary>
    public partial class SetCubeInfo : Window
    {
        public SetCubeInfoViewModel vm { get; set; }
        public SetCubeInfo(double x,double y,double z)
        {
            InitializeComponent();
            vm = new SetCubeInfoViewModel()
            {
                X=x,
                Y=y,
                Z=z,
                Width=20,
                Depth=20,
                Height=3,
                Heading=0,
            };
            DataContext = vm;
        }

        private void Draw_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }
    }
}
