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
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            
            //读取自定义菜单json
            string str = File.ReadAllText($"{Directory.GetCurrentDirectory()}\\MenuConfigs\\MenuConfig.json");
            List<CustomMenuItem> menu = JsonConvert.DeserializeObject<List<CustomMenuItem>>(str);

            //递归生成菜单栏
            foreach(var item in menu)
            {
                var it = new MenuItem() { Header = item.Title, Tag = item };
                it.Click += MenuItem_Click;
                myMenu.Items.Add(it);
                GenerateMenus(item, it);
            }

            //默认打开tab
            OpenOneTab(new CustomMenuItem() { Title = "Prompt(提示)", DllName = "WpfDemo.exe", ClassName = "WpfDemo.MenuConfigs.PromptTab" });
        }

        private void GenerateMenus(CustomMenuItem item, MenuItem it)
        {
            if (item.Children != null)
            {
                foreach (var child in item.Children)
                {
                    var c = new MenuItem() { Header = child.Title, Tag = child };
                    c.Click += MenuItem_Click;
                    it.Items.Add(c);
                    GenerateMenus(child, c);
                }
            }
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            MenuItem menuItem = sender as MenuItem;
            var data = menuItem.Tag as CustomMenuItem;
            if(!string.IsNullOrEmpty(data.DllName) && !string.IsNullOrEmpty(data.ClassName))
            {
                OpenOneTab(data);
            }
        }

        private void OpenOneTab(CustomMenuItem data)
        {
            //这里可以动态加载其他dll文件中的组件
            Assembly assem = Assembly.LoadFile($"{Directory.GetCurrentDirectory()}\\{data.DllName}");
            var onePage = assem.CreateInstance(data.ClassName);

            ClosableTab theTabItem = new ClosableTab();
            theTabItem.Content = onePage;
            theTabItem.Title = data.Title;
            myTabControl.Items.Add(theTabItem);
            theTabItem.Focus();
        }
    }
}
