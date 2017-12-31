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
using WpfDemo.PropertyChanged.Models;
using WpfDemo.PropertyChanged.ViewModels;

namespace WpfDemo.PropertyChanged
{
    /// <summary>
    /// ChangeStudentInfo.xaml 的交互逻辑
    /// </summary>
    public partial class ChangeStudentInfo : Window
    {
        ChangeStudentInfoViewModel vm = new ChangeStudentInfoViewModel();
        public ChangeStudentInfo()
        {
            InitializeComponent();
            vm.InfoViewModel= new StudentByINotifyPropertyChanged()
            {
                Name = "小芳",
                Sex = "女"
            };
            vm.TextBoxViewModel = new StudentByINotifyPropertyChanged();
            DataContext = vm;
        }

        private void ChangeClick(object sender, RoutedEventArgs e)
        {
            vm.InfoViewModel.Name = vm.TextBoxViewModel.Name;
            vm.InfoViewModel.Sex = vm.TextBoxViewModel.Sex;
        }
    }
}
