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
        

        //属性
        public Double Transparency
        {
            get { return (Double)GetValue(TransparencyDependency); }
            set { SetValue(TransparencyDependency, value); }
        }

        //注册依赖属性
        public readonly static DependencyProperty TransparencyDependency =
            DependencyProperty.Register( 
            //属性名
            "Transparency",
            //属性数据类型
            typeof(Double),
            //拥有者
            typeof(CustomBorder),
            //处理方法
            new PropertyMetadata(new PropertyChangedCallback(transparencyPropertyChangedCallback))
            );

        //当属性修改时的处理
        static void transparencyPropertyChangedCallback(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            CustomBorder border = (sender as CustomBorder);
            if (border != null)
            {
                border.Opacity = 1 - Convert.ToDouble(e.NewValue) / 255;
            }
        }
    }
}
