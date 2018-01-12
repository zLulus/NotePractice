using CefSharp;
using CefSharp.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace CefSharpWpfDemo.Handler
{
    public class MenuHandler : IContextMenuHandler
    {
        public static Window mainWindow { get; set; }
        void IContextMenuHandler.OnBeforeContextMenu(IWebBrowser browserControl, IBrowser browser, IFrame frame, IContextMenuParams parameters, IMenuModel model)
        {
            ////if (model.Count > 0)
            ////{
            ////    model.AddSeparator();
            ////}
            ////清除默认菜单栏
            //model.Clear();
            //model.AddItem((CefMenuCommand)26501, "最小化");
            //model.AddItem((CefMenuCommand)26502, "关闭");


            ////To disable context mode then clear
            //// model.Clear();
        }

        bool IContextMenuHandler.OnContextMenuCommand(IWebBrowser browserControl, IBrowser browser, IFrame frame, IContextMenuParams parameters, CefMenuCommand commandId, CefEventFlags eventFlags)
        {
            return true;
            ////最小化
            //if (commandId == (CefMenuCommand)26501)
            //{
            //    mainWindow.Dispatcher.Invoke(
            //       new Action(
            //            delegate
            //            {
            //                mainWindow.WindowState = WindowState.Minimized;
            //            }
            //       ));
            //    return true;
            //}
            //if (commandId == (CefMenuCommand)26502)   //关闭
            //{
            //    mainWindow.Dispatcher.Invoke(
            //       new Action(
            //            delegate
            //            {
            //                Application.Current.Shutdown();
            //            }
            //       ));
            //    return true;
            //}
            //return false;
        }

        void IContextMenuHandler.OnContextMenuDismissed(IWebBrowser browserControl, IBrowser browser, IFrame frame)
        {
            //隐藏菜单栏
            var chromiumWebBrowser = (ChromiumWebBrowser)browserControl;

            chromiumWebBrowser.Dispatcher.Invoke(() =>
            {
                chromiumWebBrowser.ContextMenu = null;
            });
        }

        bool IContextMenuHandler.RunContextMenu(IWebBrowser browserControl, IBrowser browser, IFrame frame, IContextMenuParams parameters, IMenuModel model, IRunContextMenuCallback callback)
        {
            //return false;

            //绘制了一遍菜单栏  所以初始化的时候不必绘制菜单栏，再此处绘制即可
            var chromiumWebBrowser = (ChromiumWebBrowser)browserControl;

            //IMenuModel is only valid in the context of this method, so need to read the values before invoking on the UI thread
            //var menuItems = GetMenuItems(model);

            chromiumWebBrowser.Dispatcher.Invoke(() =>
            {
                var menu = new ContextMenu
                {
                    IsOpen = true
                };

                RoutedEventHandler handler = null;

                handler = (s, e) =>
                {
                    menu.Closed -= handler;

                    //If the callback has been disposed then it's already been executed
                    //so don't call Cancel
                    if (!callback.IsDisposed)
                    {
                        callback.Cancel();
                    }
                };

                menu.Closed += handler;

                //foreach (var item in menuItems)
                //{
                //    menu.Items.Add(new MenuItem
                //    {
                //        Header = item.Item1,
                //        Command = MinWindow
                //    });
                //}
                menu.Items.Add(new MenuItem
                {
                    Header = "最小化",
                    Command = new CustomCommand(MinWindow)
                });
                menu.Items.Add(new MenuItem
                {
                    Header = "关闭",
                    Command = new CustomCommand(CloseWindow)
                });
                chromiumWebBrowser.ContextMenu = menu;

            });

            return true;

            //以下方法是，获取初始化的菜单栏，对其进行加载，稍微有点问题，事件不响应
            //参考：https://github.com/cefsharp/CefSharp/issues/1795
            //var chromiumWebBrowser = (ChromiumWebBrowser)browserControl;

            ////IMenuModel is only valid in the context of this method, so need to read the values before invoking on the UI thread
            //var menuItems = GetMenuItems(model);

            //chromiumWebBrowser.Dispatcher.Invoke(() =>
            //{
            //    var menu = new ContextMenu
            //    {
            //        IsOpen = true
            //    };

            //    RoutedEventHandler handler = null;

            //    handler = (s, e) =>
            //    {
            //        menu.Closed -= handler;

            //        //If the callback has been disposed then it's already been executed
            //        //so don't call Cancel
            //        if (!callback.IsDisposed)
            //        {
            //            callback.Cancel();
            //        }
            //    };

            //    menu.Closed += handler;

            //    foreach (var item in menuItems)
            //    {
            //        menu.Items.Add(new MenuItem
            //        {
            //            Header = item.Item1,
            //            Command = new RelayCommand(() => { callback.Continue(item.Item2, CefEventFlags.LeftMouseButton); })
            //        });
            //    }
            //    chromiumWebBrowser.ContextMenu = menu;

            //});

            //return true;
        }

        /// <summary>
        /// 关闭窗体
        /// </summary>
        private void CloseWindow()
        {
            //调用线程无法访问此对象,因为另一个线程拥有该对象
            //handler和window是两个线程，WPF做了线程安全。。。so以下
            mainWindow.Dispatcher.Invoke(
                new Action(
                       delegate
                       {
                           Application.Current.Shutdown();
                       }
                  ));
        }

        /// <summary>
        /// 最小化窗体
        /// </summary>
        private void MinWindow()
        {
            mainWindow.Dispatcher.Invoke(
                new Action(
                        delegate
                        {
                            mainWindow.WindowState = WindowState.Minimized;
                        }
                   ));
        }

        private static IEnumerable<Tuple<string, CefMenuCommand>> GetMenuItems(IMenuModel model)
        {
            var list = new List<Tuple<string, CefMenuCommand>>();
            for (var i = 0; i < model.Count; i++)
            {
                var header = model.GetLabelAt(i);
                var commandId = model.GetCommandIdAt(i);
                list.Add(new Tuple<string, CefMenuCommand>(header, commandId));
            }

            return list;
        }
    }
}
