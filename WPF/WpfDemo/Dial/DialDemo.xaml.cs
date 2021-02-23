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

namespace WpfDemo.Dial
{
    /// <summary>
    /// DialDemo.xaml 的交互逻辑
    /// </summary>
    public partial class DialDemo : UserControl
    {
        WpfDemo.Dial.VPN VPN { get; set; }
        DialDemoViewModel dialDemoViewModel { get; set; }
        public DialDemo()
        {
            InitializeComponent();

            VPN = new VPN();
            dialDemoViewModel = new DialDemoViewModel();
            DataContext = dialDemoViewModel;
        }


        private void Connect_Click(object sender, RoutedEventArgs e)
        {
            //断开之前的连接
            VPN.Disconnect();
            string selectedProtocol = dialDemoViewModel.Protocol.Tag.ToString();
            VPN.setParameters(dialDemoViewModel.IP, dialDemoViewModel.AdapterName, dialDemoViewModel.UserName, dialDemoViewModel.Password, selectedProtocol, dialDemoViewModel.PreSharedKey);

            var result = VPN.Connect();
            if (result)
            {
                MessageBox.Show("Connect Succeeded!");
            }
            else
            {
                MessageBox.Show("Connect Failed!");
            }
        }

        private void Disconnect_Click(object sender, RoutedEventArgs e)
        {
            VPN.Disconnect();
        }
    }
}
