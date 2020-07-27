using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
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
using WpfDemo.ClosableTabs;
using WpfDemo.MenuConfigs;

namespace WpfDemo
{
    /// <summary>
    /// StartWindow.xaml 的交互逻辑
    /// </summary>
    public partial class StartWindow : Window
    {
        public StartWindow()
        {
            InitializeComponent();

            //TODO 读取json
            List<CustomMenuItem> menu = new List<CustomMenuItem>();
            var fMenu = new CustomMenuItem()
            {
                Title = "Single Selector",
                DllName = "WpfDemo.exe",
                ClassName = "WpfDemo.SingleSelectors.SingleSelector",
                Children = new List<CustomMenuItem>()
            };
            menu.Add(fMenu);
            var s = JsonConvert.SerializeObject(menu);

            foreach(var item in menu)
            {
                var it = new MenuItem() { Header = item.Title, Tag = item };
                it.Click += It_Click;
                myMenu.Items.Add(it);

                //todo 递归
            }

           
        }

        private void It_Click(object sender, RoutedEventArgs e)
        {
            MenuItem menuItem = sender as MenuItem;
            var data = menuItem.Tag as CustomMenuItem;
            if(!string.IsNullOrEmpty(data.DllName) && !string.IsNullOrEmpty(data.ClassName))
            {
                //这里可以动态加载其他dll文件中的组件
                Assembly assem;
                assem = Assembly.LoadFile($"{Directory.GetCurrentDirectory()}\\{data.DllName}");
                var onePage = assem.CreateInstance(data.ClassName);

                ClosableTab theTabItem = new ClosableTab();
                theTabItem.Content = onePage;
                theTabItem.Title = data.Title;
                myTabControl.Items.Add(theTabItem);
                theTabItem.Focus();
            }

           
        }
    }
}
