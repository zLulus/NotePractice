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

namespace WpfDemo.NotShowInTaskbarWindows
{
    /// <summary>
    /// NotShowInTaskbarTab.xaml 的交互逻辑
    /// </summary>
    public partial class NotShowInTaskbarTab : UserControl
    {
        public NotShowInTaskbarTab()
        {
            InitializeComponent();
        }

        private void OpenNotShowInTaskbarWindow_Click(object sender, RoutedEventArgs e)
        {
            NotShowInTaskbarWindow window = new NotShowInTaskbarWindow(false);
            window.ShowDialog();
        }

        private void OpenNotShowInTaskbarWindowWithOwner1_Click(object sender, RoutedEventArgs e)
        {
            NotShowInTaskbarWindow window = new NotShowInTaskbarWindow(true);
            window.ShowDialog();
        }

        private void OpenNotShowInTaskbarWindowWithOwner2_Click(object sender, RoutedEventArgs e)
        {
            NotShowInTaskbarWindow window = new NotShowInTaskbarWindow(true, MainWindow.Instance);
            window.ShowDialog();
            
        }
    }
}
