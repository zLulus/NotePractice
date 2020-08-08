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

namespace WpfDemo.Tag
{
    /// <summary>
    /// TagDemo.xaml 的交互逻辑
    /// </summary>
    public partial class TagDemo : UserControl
    {
        public TagDemo()
        {
            InitializeComponent();
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            //这里可以看出Tag是object，可以存放任意数据类型
            string tag = (sender as Button).Tag as string;
            MessageBox.Show($"Tag的内容是\"{tag}\"");
        }
    }
}
