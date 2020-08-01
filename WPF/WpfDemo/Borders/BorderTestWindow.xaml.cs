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

namespace WpfDemo.Borders
{
    /// <summary>
    /// BorderTestWindow.xaml 的交互逻辑
    /// </summary>
    public partial class BorderTestWindow : UserControl
    {
        public BorderTestWindow()
        {
            InitializeComponent();
        }

        private void borderWithBackground_MouseDown(object sender, MouseButtonEventArgs e)
        {
            MessageBox.Show("borderWithBackground_MouseDown");
        }

        private void borderWithoutBackground_MouseDown(object sender, MouseButtonEventArgs e)
        {
            MessageBox.Show("borderWithoutBackground_MouseDown");
        }
    }
}
