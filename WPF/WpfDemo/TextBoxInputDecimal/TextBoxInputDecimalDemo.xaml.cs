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
using WpfDemo.TextBoxInputDecimal.ViewModels;

namespace WpfDemo.TextBoxInputDecimal
{
    /// <summary>
    /// TextBoxInputDecimalDemo.xaml 的交互逻辑
    /// </summary>
    public partial class TextBoxInputDecimalDemo : UserControl
    {
        //https://stackoverflow.com/questions/18964518/wpf-converters-with-different-decimal-number
        TextBoxInputDecimalViewModel vm { get; set; }
        public TextBoxInputDecimalDemo()
        {
            InitializeComponent();

            vm = new TextBoxInputDecimalViewModel();
            DataContext = vm;
        }

        private void GetNumber_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show($"You input: {vm.InputNumber}");
        }
    }
}
