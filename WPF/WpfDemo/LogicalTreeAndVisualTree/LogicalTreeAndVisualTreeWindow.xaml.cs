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
using WpfDemo.LogicalTreeAndVisualTree.Tools;

namespace WpfDemo.LogicalTreeAndVisualTree
{
    /// <summary>
    /// LogicalTreeAndVisualTreeWindow.xaml 的交互逻辑
    /// </summary>
    public partial class LogicalTreeAndVisualTreeWindow : Window
    {
        public LogicalTreeAndVisualTreeWindow()
        {
            InitializeComponent();
        }

        private void GetLogicalTree_Click(object sender, RoutedEventArgs e)
        {
            var l1 = LogicalTreeTool.FindChildren<DependencyObject>(this).ToList();
            var l2 = LogicalTreeTool.FindLogicalChildren<DependencyObject>(this).ToList();
            var l3 = LogicalTreeTool.FindLogicalChildren2<DependencyObject>(this).ToList();
        }

        private void GetVisualTree_Click(object sender, RoutedEventArgs e)
        {
            var p = VisualTreeTool.FindParent<DependencyObject>(this,typeof(DependencyObject));
            var v1 = VisualTreeTool.FindInVisualTreeDown(this, typeof(DependencyObject));
            var v2 = VisualTreeTool.FindInVisualTreeDown2(this, typeof(DependencyObject)).ToList();
            var v3 = VisualTreeTool.FindVisualChildren<DependencyObject>(this).ToList();
            var v4 = VisualTreeTool.FindVisualChildren2<DependencyObject>(this).ToList();
        }
    }
}
