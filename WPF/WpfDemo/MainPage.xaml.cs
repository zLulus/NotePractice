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
using WpfDemo.Navigation;

namespace WpfDemo
{
    /// <summary>
    /// MainPage.xaml 的交互逻辑
    /// </summary>
    public partial class MainPage : Page
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private void ShowKeyboard(object sender, RoutedEventArgs e)
        {
            InputNumberWindow window = new InputNumberWindow();
            window.Show();
        }

        private void PagePop(object sender, RoutedEventArgs e)
        {
            ccl.Content = new PopUserControl(this);
        }

        private void UserControChange(object sender, RoutedEventArgs e)
        {
            ccl.Content = new PopChangeUserControl(this);
        }
    }
}
