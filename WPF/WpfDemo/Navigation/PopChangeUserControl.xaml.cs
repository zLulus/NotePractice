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

namespace WpfDemo.Navigation
{
    /// <summary>
    /// PopChangeUserControl.xaml 的交互逻辑
    /// </summary>
    public partial class PopChangeUserControl : UserControl
    {
        MainPage mainPage;
        public PopChangeUserControl(MainPage _mainPage)
        {
            InitializeComponent();
            mainPage = _mainPage;
        }

        private void GoNext(object sender, RoutedEventArgs e)
        {
            mainPage.ccl.Content = new PopUserControl(mainPage);
        }
    }
}
