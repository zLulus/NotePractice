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
using System.Windows.Threading;

namespace WpfDemo.PauseAndResumeThread
{
    /// <summary>
    /// PauseAndResumeThreadWindow.xaml 的交互逻辑
    /// </summary>
    public partial class PauseAndResumeThreadWindow : Window
    {
        //https://docs.microsoft.com/zh-cn/dotnet/api/system.threading.thread?view=netcore-3.1
        //https://docs.microsoft.com/zh-cn/dotnet/standard/threading/pausing-and-resuming-threads
        Thread thread;
        public PauseAndResumeThreadWindow()
        {
            InitializeComponent();
        }
        private void Start_Click(object sender, RoutedEventArgs e)
        {
            thread = new Thread(() => { BackgroundWorker_DoWork(); });
            thread.IsBackground = true;
            thread.Start();
        }

        private void BackgroundWorker_DoWork()
        {
            int i = 0;
            while (true)
            {
                this.Dispatcher.Invoke(DispatcherPriority.Normal, (ThreadStart)delegate ()
                {
                    timeLabel.Content = DateTime.Now.ToString();
                });
                i++;
                this.Dispatcher.Invoke(DispatcherPriority.Normal, (ThreadStart)delegate ()
                {
                    numberLabel.Content = i;
                });

                Thread.Sleep(1000);
            }

        }

        private void Pause_Click(object sender, RoutedEventArgs e)
        {
            thread.Suspend();
        }

        private void Resume_Click(object sender, RoutedEventArgs e)
        {
            thread.Resume();
        }

        private void Cancle_Click(object sender, RoutedEventArgs e)
        {
            thread.Abort();
        }
    }
}
