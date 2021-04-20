using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using WpfDemo.ListViewWithCheckBox.ViewModels;

namespace WpfDemo.ListViewWithCheckBox
{
    /// <summary>
    /// ListViewWithCheckBoxDemo.xaml 的交互逻辑
    /// </summary>
    public partial class ListViewWithCheckBoxDemo : UserControl
    {
        //https://stackoverflow.com/questions/28546582/wpf-listview-header-checkbox-and-mvvm-command
        ObservableCollection<MyTask> vm { get; set; }
        public ListViewWithCheckBoxDemo()
        {
            InitializeComponent();

            vm = new ObservableCollection<MyTask>();
            vm.Add(new MyTask() { TaskName = "任务1", IsChecked = false });
            vm.Add(new MyTask() { TaskName = "任务2", IsChecked = true });

            taskListBox.DataContext = vm;
        }

        private void SelectAll_Checked(object sender, RoutedEventArgs e)
        {
            foreach(var data in vm)
            {
                data.IsChecked = true;
            }
        }

        private void SelectAll_Unchecked(object sender, RoutedEventArgs e)
        {
            foreach (var data in vm)
            {
                data.IsChecked = false;
            }
        }
    }
}
