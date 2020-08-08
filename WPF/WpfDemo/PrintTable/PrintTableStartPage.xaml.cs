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

namespace WpfDemo.PrintTable
{
    /// <summary>
    /// PrintTableStartPage.xaml 的交互逻辑
    /// </summary>
    public partial class PrintTableStartPage : UserControl
    {
        public PrintTableStartPage()
        {
            InitializeComponent();
        }

        private void OpenWindow_Click(object sender, RoutedEventArgs e)
        {
            PrintTableWindow window = new PrintTableWindow();
            window.Show();
        }
    }
}
