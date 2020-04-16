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

namespace WpfDemo.MoveAndResizeControl.Dialogs
{
    /// <summary>
    /// ModifyTextDialog.xaml 的交互逻辑
    /// </summary>
    public partial class ModifyTextDialog : Window
    {
        public ModifyTextDialog(string text)
        {
            InitializeComponent();
            myTextBox.Text = text;
        }

        private void OkClick(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
            this.Close();
        }

        private void CancleClick(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }
    }
}
