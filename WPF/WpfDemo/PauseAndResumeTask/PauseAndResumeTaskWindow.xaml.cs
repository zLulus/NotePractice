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
using System.Threading;

namespace WpfDemo.PauseAndResumeTask
{
    /// <summary>
    /// PauseAndResumeTaskWindow.xaml 的交互逻辑
    /// </summary>
    public partial class PauseAndResumeTaskWindow : Window
    {
        PauseTokenSource pts;
        Task task;
        public PauseAndResumeTaskWindow()
        {
            InitializeComponent();
        }

        private async void Test_Click(object sender, RoutedEventArgs e)
        {
            await Test(CancellationToken.None);
        }

        async Task Test(CancellationToken token)
        {
            while (true)
            {
                token.ThrowIfCancellationRequested();

                Console.WriteLine("Press enter to pause...");
                Console.ReadLine();

                Console.WriteLine("Before pause requested");
                await pts.PauseAsync();
                Console.WriteLine("After pause requested, paused: " + await pts.IsPaused());

                Console.WriteLine("Press enter to resume...");
                Console.ReadLine();

                Console.WriteLine("Before resume");
                await pts.ResumeAsync();
                Console.WriteLine("After resume");
            }
        }

        public async Task DoWorkAsync(PauseToken pause, CancellationToken token)
        {
            try
            {
                while (true)
                {
                    token.ThrowIfCancellationRequested();
                    timeLabel.Content = DateTime.Now.ToString();
                    Console.WriteLine("Before await pause.PauseIfRequestedAsync()");
                    await pause.PauseIfRequestedAsync();
                    Console.WriteLine("After await pause.PauseIfRequestedAsync()");

                    await Task.Delay(1000);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: {0}", e);
                throw;
            }
        }

        private async void Start_Click(object sender, RoutedEventArgs e)
        {
            var token = CancellationToken.None;
            pts = new PauseTokenSource();
            task = DoWorkAsync(pts.Token, token);

            token.ThrowIfCancellationRequested();
        }

        private async void Pause_Click(object sender, RoutedEventArgs e)
        {
            await pts.PauseAsync();
            var b = await pts.IsPaused();
        }

        private async void Resume_Click(object sender, RoutedEventArgs e)
        {
            await pts.ResumeAsync();
        }
    }
}
