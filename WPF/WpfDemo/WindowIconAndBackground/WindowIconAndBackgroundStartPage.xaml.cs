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

namespace WpfDemo.WindowIconAndBackground
{
    /// <summary>
    /// WindowIconAndBackgroundStartPage.xaml 的交互逻辑
    /// </summary>
    public partial class WindowIconAndBackgroundStartPage : UserControl
    {
        public WindowIconAndBackgroundStartPage()
        {
            InitializeComponent();
        }

        private void OpenWindow_Click(object sender, RoutedEventArgs e)
        {
            WindowIconAndBackgroundDemo window = new WindowIconAndBackgroundDemo();
            window.Show();
        }
    }
}
