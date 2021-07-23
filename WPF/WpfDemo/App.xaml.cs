using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using WpfDemo.Tools;

namespace WpfDemo
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            //wpf 程序异常捕获，而不崩溃退出
            DispatcherUnhandledException += App_DispatcherUnhandledException;
            TaskScheduler.UnobservedTaskException += TaskScheduler_UnobservedTaskException;
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
        }
        private void TaskScheduler_UnobservedTaskException(object sender, UnobservedTaskExceptionEventArgs e)
        {
            e.SetObserved();
            ExceptionHandler(e.Exception.InnerException);
        }

        private void App_DispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            e.Handled = true;
            ExceptionHandler(e.Exception);
        }

        private void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            ExceptionHandler((Exception)e.ExceptionObject);
        }

        private void ExceptionHandler(Exception ex)
        {
            Dispatcher.Invoke(() =>
            {
#if DEBUG
                MessageBox.Show(string.Format("调用堆栈：\n{0}\n\n\n异常信息：\n{1}", ex.StackTrace, ex.Message));
#else
                MessageBox.Show(string.Format("异常信息：\n{0}",  ex.Message));
#endif  
            });

            RecordException(ex,true);
        }

        private static void RecordException(Exception ex, bool isTop)
        {
            var logger = LogTool.GetFileLogger($"异常日志");
            logger.Info($"Message：{ex.Message}");
            logger.Info($"ToString：{ex.ToString()}");
            logger.Info($"Source：{ex.Source}");
            logger.Info($"StackTrace：{ex.StackTrace}");
            if (ex.InnerException != null)
            {
                RecordException(ex.InnerException, false);
            }
            if (isTop)
            {
                logger.Info("\n");
                logger.Info("\n");
            }
        }
    }
}
