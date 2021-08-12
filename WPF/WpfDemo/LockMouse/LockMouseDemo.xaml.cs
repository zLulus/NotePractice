using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
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

namespace WpfDemo.LockMouse
{
    /// <summary>
    /// LockMouseDemo.xaml 的交互逻辑
    /// </summary>
    public partial class LockMouseDemo : UserControl
    {
        //https://stackoverflow.com/questions/4016933/c-sharp-block-mouse-movement
        public LockMouseDemo()
        {
            InitializeComponent();
        }

        [DllImport("user32.dll")]
        private static extern bool BlockInput(bool block);

        private void LockMouse_Click(object sender, RoutedEventArgs e)
        {
            BlockInput(true);
            Thread.Sleep(3000);
            BlockInput(false);
        }
    }
}
