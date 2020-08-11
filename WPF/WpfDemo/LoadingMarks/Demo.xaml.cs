using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using WpfDemo.LoadingMarks.ViewModels;

namespace WpfDemo.LoadingMarks
{
    /// <summary>
    /// Demo.xaml 的交互逻辑
    /// </summary>
    public partial class Demo : UserControl
    {
        LoadingMarkViewModel vm = new LoadingMarkViewModel();
        public Demo()
        {
            InitializeComponent();

            DataContext = vm;
        }

      

        private void Switch_Click(object sender, RoutedEventArgs e)
        {
            if (vm.Loading == Visibility.Visible)
            {
                vm.Loading = Visibility.Hidden;
                Mouse.OverrideCursor = Cursors.Arrow;
            }
            else
            {
                vm.Loading = Visibility.Visible;
                Mouse.OverrideCursor = Cursors.Wait;
            }
        }
    }
}
