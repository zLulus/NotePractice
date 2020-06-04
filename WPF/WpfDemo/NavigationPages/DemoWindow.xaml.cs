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

namespace WpfDemo.NavigationPages
{
    /// <summary>
    /// DemoWindow.xaml 的交互逻辑
    /// </summary>
    public partial class DemoWindow : Window
    {
        public DemoWindow()
        {
            InitializeComponent();
        }

        private void Window_Load(object sender, RoutedEventArgs e)
        {
            //自定义导航页
            List<NavigationPageContentInfo> navigationPageContentInfos = new List<NavigationPageContentInfo>();
            navigationPageContentInfos.Add(new NavigationPageContentInfo() { Content = new DepartmentView(), Order = 1 });
            navigationPageContentInfos.Add(new NavigationPageContentInfo() { Content = new EmployeeView(), Order = 2 });
            //vm
            var vm = new TotalViewModel() { DepartmentId = 1, DepartmentName = "部门", EmployeeId = 2, EmployeeName = "员工" };

            grid.Children.Add(new NavigationPage(navigationPageContentInfos, vm));
        }
    }
}
