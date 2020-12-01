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

namespace WpfDemo.GenerateControls
{
    /// <summary>
    /// GenerateControl.xaml 的交互逻辑
    /// </summary>
    public partial class GenerateControl : UserControl
    {
        Point p;

        public GenerateControl()
        {
            InitializeComponent();
        }

        private void GenerateControl_Click(object sender, RoutedEventArgs e)
        {
            if (generateControlGrid.Children.Count != 0)
            {
                MessageBox.Show("请先清除控件");
                return;
            }
            TextBlock textBlock = new TextBlock();
            textBlock.Text = "Test";
            textBlock.Margin = new Thickness(p.X , p.Y , 0, 0);
            //textBlock.Margin = new Thickness(p.X+ translateTransform.X, p.Y+ translateTransform.Y, 0, 0);
            //textBlock.SetValue(Canvas.LeftProperty, p.X);
            //textBlock.SetValue(Canvas.TopProperty, p.Y);
            generateControlGrid.Children.Add(textBlock);
        }

        private void GetLocation_Click(object sender, RoutedEventArgs e)
        {
            // Get absolute location on screen of upper left corner of button
            Point locationFromScreen = this.locationTextBlock.PointToScreen(new Point(0, 0));
            // Transform screen point to WPF device independent point
            PresentationSource source = PresentationSource.FromVisual(generateControlGrid);

            System.Windows.Point targetPoints = source.CompositionTarget.TransformFromDevice.Transform(locationFromScreen);

            Point generateControlGridLocation = this.generateControlGrid.PointToScreen(new Point(0, 0));

            p = new Point(locationFromScreen.X - generateControlGridLocation.X, locationFromScreen.Y - generateControlGridLocation.Y);

            MessageBox.Show($"({p.X},{p.Y})");
        }

        private void Clear_Click(object sender, RoutedEventArgs e)
        {
            generateControlGrid.Children.Clear();
        }
    }
}
