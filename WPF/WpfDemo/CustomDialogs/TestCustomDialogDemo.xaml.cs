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

namespace WpfDemo.CustomDialogs
{
    /// <summary>
    /// Interaction logic for TestCustomDialogDemo.xaml
    /// </summary>
    public partial class TestCustomDialogDemo : UserControl
    {
        public TestCustomDialogDemo()
        {
            InitializeComponent();
        }

        private void OpenCustomDialog_Click(object sender, RoutedEventArgs e)
        {
            CustomDialog dialog = new CustomDialog(new TestCustomDialogContentControl(), "自定义弹窗");
            dialog.ShowDialog(()=> 
            {
                return true;
            });
        }
    }
}
