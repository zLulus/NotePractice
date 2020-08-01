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

namespace WpfDemo.ContextMenus
{
    /// <summary>
    /// CreateContextMenuWindow.xaml 的交互逻辑
    /// </summary>
    public partial class CreateContextMenuWindow : UserControl
    {
        public CreateContextMenuWindow()
        {
            InitializeComponent();

            //添加菜单栏
            var menu = new MenuItem();
            menu.Header = "menu1";
            menu.Click += (s, e) =>
            {
                //do something
            };
            //menu.IsEnabled = true;
            myBorder2.ContextMenu = new ContextMenu();
            myBorder2.ContextMenu.Items.Add(menu);
        }

        private void DeleteClick(object sender, RoutedEventArgs e)
        {

        }
    }
}
