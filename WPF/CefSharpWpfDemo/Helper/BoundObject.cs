using CefSharp;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace CefSharpWpfDemo.Helper
{
    public class BoundObject
    {
        private Window mainWindow { get; set; }
        public BoundObject(Window _mainWindow)
        {
            mainWindow = _mainWindow;
        }

        /// <summary>
        /// 打开exe文件
        /// </summary>
        public void OpenFile()
        {
            try
            {
                //MessageBox.Show("(\"This is an MyMethod coming from C#\")", "haha");
                string path = ConfigurationManager.AppSettings["filePath"];
                System.Diagnostics.Process.Start(path);
            }
            catch(Exception ex)
            {
                MessageBox.Show("请输入正确的文件路径！", "提示");
            }
        }

        /// <summary>
        /// 最小化窗体
        /// </summary>
        public void MinWindow()
        {
            mainWindow.Dispatcher.Invoke(
                new Action(
                        delegate
                        {
                            mainWindow.WindowState = WindowState.Minimized;
                        }
                   ));
        }
    }
}
