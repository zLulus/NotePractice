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
using System.Runtime.InteropServices;
using System.Windows.Interop;
using System.Windows.Forms.Integration;
using System.Data;
using System.Configuration;
using System.Diagnostics;
using CefSharp;
using CefSharpWpfDemo.Handler;

namespace CefSharpWpfDemo
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            //在js里面注册对象bound，然后用该对象调用C#方法
            //webBrowser.RegisterAsyncJsObject("bound", new BoundObject(this), BindingOptions.DefaultBinder); //Use the default binder to serialize values into complex objects
            //隐藏滚动条
            webBrowser.FrameLoadEnd += OnBrowserFrameLoadEnd;
            //右键菜单栏
            MenuHandler.mainWindow = this;
            webBrowser.MenuHandler = new MenuHandler();
            
        }

        private void OnBrowserFrameLoadEnd(object sender, FrameLoadEndEventArgs args)
        {
            if (args.Frame.IsMain)
            {
                args
                    .Browser
                    .MainFrame
                    .ExecuteJavaScriptAsync(
                    "document.body.style.overflow = 'hidden'");
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            webBrowser.Address = ConfigurationManager.AppSettings["url"];
        }
    }
}
