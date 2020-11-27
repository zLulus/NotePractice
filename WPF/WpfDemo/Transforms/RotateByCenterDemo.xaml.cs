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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfDemo.Transforms
{
    /// <summary>
    /// Interaction logic for RotateByCenterDemo.xaml
    /// </summary>
    public partial class RotateByCenterDemo : UserControl
    {
        public RotateByCenterDemo()
        {
            InitializeComponent();
        }

        private void backgroundSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            RotateTransform rt = new RotateTransform();
            rt.Angle = backgroundSlider.Value;
            //RenderTransformOrigin="0.5,0.5"
            backgroundeButton.RenderTransform = rt;
        }
    }
}
