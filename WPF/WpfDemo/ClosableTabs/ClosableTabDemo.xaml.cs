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
using WpfDemo.SingleSelectors;

namespace WpfDemo.ClosableTabs
{
    /// <summary>
    /// ClosableTabDemo.xaml 的交互逻辑
    /// </summary>
    public partial class ClosableTabDemo : Window
    {
        public ClosableTabDemo()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            ClosableTab theTabItem = new ClosableTab();
            theTabItem.Title = "Small title";
            tabControl1.Items.Add(theTabItem);
            theTabItem.Focus();
        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            ClosableTab theTabItem = new ClosableTab();
            theTabItem.Title = "A much longer title for an example";
            tabControl1.Items.Add(theTabItem);
            theTabItem.Focus();

        }

        private void button3_Click(object sender, RoutedEventArgs e)
        {
            Assembly assem;
            assem = Assembly.LoadFile($"{Directory.GetCurrentDirectory()}\\WpfDemo.exe");
            var onePage = assem.CreateInstance("WpfDemo.SingleSelectors.SingleSelector");

            ClosableTab theTabItem = new ClosableTab();
            theTabItem.Content = onePage;
            theTabItem.Title = "Single Selector";
            tabControl1.Items.Add(theTabItem);
            theTabItem.Focus();
        }
    }
}
