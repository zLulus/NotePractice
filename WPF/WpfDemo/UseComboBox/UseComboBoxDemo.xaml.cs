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
using WpfDemo.UseComboBox.ViewModels;

namespace WpfDemo.UseComboBox
{
    /// <summary>
    /// UseComboBoxDemo.xaml 的交互逻辑
    /// </summary>
    public partial class UseComboBoxDemo : UserControl
    {
        UseComboBoxViewModel vm { get; set; }
        public UseComboBoxDemo()
        {
            InitializeComponent();

            vm = new UseComboBoxViewModel() { Fruits = new ObservableCollection<FruitViewModel>() };
            vm.Fruits.Add(new FruitViewModel() { Id = 1, Name = "Apple" });
            vm.Fruits.Add(new FruitViewModel() { Id = 2, Name = "Pear" });
            vm.Fruits.Add(new FruitViewModel() { Id = 3, Name = "Banana" });
            DataContext = vm;
        }
    }
}
