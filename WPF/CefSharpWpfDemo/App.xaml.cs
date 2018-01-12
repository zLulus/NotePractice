using System.Windows;
using System;
using System.Runtime.CompilerServices;
using CefSharp;
using System.IO;
using System.Reflection;
using System.Windows.Threading;
using CefSharpWpfDemo.Log;

namespace CefSharpWpfDemo
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            // Add Custom assembly resolver
            AppDomain.CurrentDomain.AssemblyResolve += Resolver;
            //处理UI线程异常
            Application.Current.DispatcherUnhandledException += CurrentDomain_UnhandledException;
            //Any CefSharp references have to be in another method with NonInlining
            // attribute so the assembly rolver has time to do it's thing.
            InitializeCefSharp();
        }

        private static void CurrentDomain_UnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            var str = "";
            var error = e.Exception;
            var strDateInfo = "出现应用程序未处理的异常：" + DateTime.Now + "\r\n";
            if (error != null)
            {
                str = string.Format(strDateInfo + "Application UnhandledException:{0};\n\r堆栈信息:{1}", error.Message,
                    error.StackTrace);
                var logger= NLogHelper.GetFileLogger("Exception");
                logger.Error(str);
            }
            else
            {
                str = string.Format("Application UnhandledError:{0}", e);
                var logger = NLogHelper.GetFileLogger("Exception");
                logger.Error(str);
            }
            MessageBox.Show("很抱歉，当前程序遇到一些问题，该操作已终止，请检查网络连接，如果问题依然存在，请联系管理员", "意外的操作", MessageBoxButton.OK,
                MessageBoxImage.Information);
            e.Handled = true;
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        private static void InitializeCefSharp()
        {
            var settings = new CefSettings();

            // Set BrowserSubProcessPath based on app bitness at runtime
            settings.BrowserSubprocessPath = Path.Combine(AppDomain.CurrentDomain.SetupInformation.ApplicationBase,
                                                   Environment.Is64BitProcess ? "x64" : "x86",
                                                   "CefSharp.BrowserSubprocess.exe");

            // Make sure you set performDependencyCheck false
            Cef.Initialize(settings, performDependencyCheck: false, browserProcessHandler: null);
        }

        // Will attempt to load missing assembly from either x86 or x64 subdir
        // Required by CefSharp to load the unmanaged dependencies when running using AnyCPU
        private static Assembly Resolver(object sender, ResolveEventArgs args)
        {
            if (args.Name.StartsWith("CefSharp"))
            {
                string assemblyName = args.Name.Split(new[] { ',' }, 2)[0] + ".dll";
                string archSpecificPath = Path.Combine(AppDomain.CurrentDomain.SetupInformation.ApplicationBase,
                                                       Environment.Is64BitProcess ? "x64" : "x86",
                                                       assemblyName);

                return File.Exists(archSpecificPath)
                           ? Assembly.LoadFile(archSpecificPath)
                           : null;
            }

            return null;
        }
    }
}
