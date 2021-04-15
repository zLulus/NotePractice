using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfDemo.RestartProcess
{
    /// <summary>
    /// RestartProcessDemo.xaml 的交互逻辑
    /// </summary>
    public partial class RestartProcessDemo : UserControl
    {
        public RestartProcessDemo()
        {
            InitializeComponent();
        }

        private void RestartProcess_Click(object sender, RoutedEventArgs e)
        {
            //重启程序
            Application.Current.Dispatcher.Invoke(() =>
            {
                Application.Current.Shutdown();


                ProcessStartInfo startInfo = new ProcessStartInfo(System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName)
                {
                    CreateNoWindow = true,
                    UseShellExecute = false
                };

                Process.Start(startInfo);
            });
        }
    }
}
