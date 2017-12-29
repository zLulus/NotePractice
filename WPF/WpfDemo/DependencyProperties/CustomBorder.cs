using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace WpfDemo.DependencyProperties
{
    public class CustomBorder : Border
    {
        public CustomBorder()
        {
            //初始化时默认给定一个背景色 
            Background = Brushes.Blue;
        }
        public readonly static DependencyProperty TransparencyDependency =
            DependencyProperty.Register( 
            "Transparency",
            typeof(Double),
            typeof(CustomBorder),
            new PropertyMetadata(new PropertyChangedCallback(transparencyPropertyChangedCallback))
            );

        static void transparencyPropertyChangedCallback(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            CustomBorder border = (sender as CustomBorder);
            if (border != null)
            {
                border.Opacity = 1 - Convert.ToDouble(e.NewValue) / 255;
            }
        }

        public Double Transparency
        {
            get { return (Double)GetValue(TransparencyDependency); }
            set { SetValue(TransparencyDependency, value); }
        }
    }
}
