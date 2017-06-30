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

namespace WpfDemo.Navigation
{
    /// <summary>
    /// PopUserControl.xaml 的交互逻辑
    /// </summary>
    public partial class PopUserControl : UserControl
    {
        System.Windows.Threading.DispatcherTimer timer = new System.Windows.Threading.DispatcherTimer();
        MainPage mainPage;
        public PopUserControl(MainPage _mainPage)
        {
            InitializeComponent();
            mainPage = _mainPage;

            timer.Tick += new EventHandler(OnTimedEvent);
            timer.Interval = new TimeSpan(0, 0, 2);
            timer.Start();
        }

        /// <summary>
        /// Timer的Elapsed事件处理程序
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        private void OnTimedEvent(object sender, EventArgs e)
        {
            //关闭计时器
            timer.Stop();
            this.Visibility = Visibility.Hidden;
            //设置Page KeepAlive=true  解决 NavigationService=null（已经被释放）的问题
            //https://stackoverflow.com/questions/1400463/wpf-navigationservice-is-null-after-2nd-page-visit
            //https://msdn.microsoft.com/en-us/library/ms750478.aspx
            if (mainPage.NavigationService != null)
            {
                mainPage.NavigationService.Navigate(new Uri("Navigation/SecondPage.xaml", UriKind.Relative));
            }
        }
    }
}
