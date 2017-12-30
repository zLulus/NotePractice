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

namespace WpfDemo
{
    /// <summary>
    /// Keyboard.xaml 的交互逻辑
    /// </summary>
    public partial class Keyboard : System.Windows.Controls.UserControl
    {
        TextBox textBox;
        Canvas grid;
        public Keyboard(TextBox _textBox, Canvas _grid)
        {
            InitializeComponent();
            textBox = _textBox;
            grid = _grid;
        }

        /// <summary>
        /// 关闭/确定
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Close(object sender, RoutedEventArgs e)
        {
            grid.Visibility = Visibility.Hidden;
        }

        /// <summary>
        /// 数字
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Number_Click(object sender, RoutedEventArgs e)
        {
            Button bt = sender as Button;
            textBox.Text += bt.Content;
        }

        /// <summary>
        /// 回退
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Back_Click(object sender, RoutedEventArgs e)
        {
            int count = textBox.Text.Count();
            if (count > 0)
            {
                textBox.Text = textBox.Text.Substring(0, count - 1);
            }
        }
    }
}
