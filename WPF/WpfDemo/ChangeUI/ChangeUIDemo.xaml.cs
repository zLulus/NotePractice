using System;
using System.Collections.Generic;
using System.Linq;
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
using System.Windows.Shapes;

namespace WpfDemo.ChangeUI
{
    /// <summary>
    /// ChangeUIDemo.xaml 的交互逻辑
    /// </summary>
    public partial class ChangeUIDemo : UserControl
    {
        public ChangeUIDemo()
        {
            InitializeComponent();

            NameLabel.Content = "我用UI线程修改了NameLabel的文字";

            Task t = new Task(() =>
            {
                //wrong
                //NameLabel.Content = "我用非UI线程修改NameLabel的文字，失败了";

                NameLabel.Dispatcher.Invoke(new Action(delegate
                {
                    NameLabel.Content = "我用UI线程修改了NameLabel的文字";
                }));
                Thread.Sleep(5000);
                this.Dispatcher.Invoke(new Action(() =>
                {
                    NameLabel.Content = "我用UI线程修改了NameLabel的文字-2";
                }));
                Thread.Sleep(5000);
                this.Dispatcher.BeginInvoke(new Action(() =>
                {
                    NameLabel.Content = "我用UI线程修改了NameLabel的文字-3";
                }));

            });
            t.Start();
        }
    }
}
