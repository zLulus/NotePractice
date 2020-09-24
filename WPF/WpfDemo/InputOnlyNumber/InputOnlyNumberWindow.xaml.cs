using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WpfDemo.InputOnlyNumber
{
    /// <summary>
    /// InputOnlyNumberWindow.xaml 的交互逻辑
    /// </summary>
    public partial class InputOnlyNumberWindow : UserControl
    {
        public InputOnlyNumberWindow()
        {
            InitializeComponent();
        }

        private void Text_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            //https://blog.csdn.net/qq_37246121/article/details/79528588
            //正则表达式校验只能输入数字
            Regex re = new Regex("[^0-9.-]+");
            e.Handled = re.IsMatch(e.Text);
        }
    }
}
