using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WpfDemo.NotShowInTaskbarWindows
{
    /// <summary>
    /// NotShowInTaskbarWindow.xaml 的交互逻辑
    /// </summary>
    public partial class NotShowInTaskbarWindow : Window
    {
        //GetForegroundWindow API
        [DllImport("user32.dll")]
        static extern IntPtr GetForegroundWindow();

        //从Handle中获取Window对象
        static Window GetWindowFromHwnd(IntPtr hwnd)
        {
            var window = HwndSource.FromHwnd((IntPtr)hwnd);
            dynamic customWindow = window.RootVisual;
            Console.WriteLine($"{customWindow == null}");
            return customWindow;
        }

        //调用GetForegroundWindow然后调用GetWindowFromHwnd
        static Window GetTopWindow()
        {
            var hwnd = GetForegroundWindow();
            if (hwnd == null)
                return null;

            return GetWindowFromHwnd(hwnd);
        }

        public NotShowInTaskbarWindow(bool isSetOwner,Window window=null)
        {
            InitializeComponent();
            txtTextBlock.Text= Guid.NewGuid().ToString();
            if (isSetOwner)
            {
                if (window != null)
                {
                    Owner = window;
                }
                else
                {
                    Owner = GetTopWindow();
                    //如果有传参UserControl作为内容，可以尝试使用Window.GetWindow(DependencyObject) 方法
                }
            }
        }
    }
}
