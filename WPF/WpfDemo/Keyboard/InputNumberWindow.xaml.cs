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

namespace WpfDemo
{
    /// <summary>
    /// InputNumberWindow.xaml 的交互逻辑
    /// </summary>
    public partial class InputNumberWindow : Window
    {
        public InputNumberWindow()
        {
            InitializeComponent();

            PhoneNumberInputGrid.Visibility = Visibility.Hidden;
            CodeInputGrid.Visibility = Visibility.Hidden;
            PhoneNumberInputGrid.Children.Add(new Keyboard(PhoneNumberTextBox, PhoneNumberInputGrid));
            CodeInputGrid.Children.Add(new Keyboard(CodeTextBox, CodeInputGrid));
        }

        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (PhoneNumberTextBox.IsFocused)
            {
                PhoneNumberInputGrid.Visibility = Visibility.Visible;
                CodeInputGrid.Visibility = Visibility.Hidden;
            }
            else if (CodeTextBox.IsFocused)
            {
                PhoneNumberInputGrid.Visibility = Visibility.Hidden;
                CodeInputGrid.Visibility = Visibility.Visible;
            }
        }

        private void Cancle_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Go_Click(object sender, RoutedEventArgs e)
        {

        }

        private void SendMessage_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
