using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using WpfDemo.UseComboBox.ViewModels;

namespace WpfDemo.UseComboBox
{
    /// <summary>
    /// SettingWithComboBoxDemo.xaml 的交互逻辑
    /// </summary>
    public partial class SettingWithComboBoxDemo : UserControl
    {
        SettingWithComboBoxDemoViewModel vm { get; set; }
        public SettingWithComboBoxDemo()
        {
            InitializeComponent();

            vm = new SettingWithComboBoxDemoViewModel();
            vm.Fruits = new ObservableCollection<FruitViewModel>();
            vm.Fruits.Add(new FruitViewModel() { Id = 1, Name = "Apple" });
            vm.Fruits.Add(new FruitViewModel() { Id = 2, Name = "Pear" });
            vm.Fruits.Add(new FruitViewModel() { Id = 3, Name = "Banana" });
            //设置选项，反显
            vm.SelectFruit = vm.Fruits.FirstOrDefault(x => x.Id == 1);

            DataContext = vm;
        }

        private void GetCurrentSelectItem_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if(vm.SelectFruit!=null)
                MessageBox.Show($"{vm.SelectFruit.Name}");
            else
                MessageBox.Show($"None(没有选项)");
        }

        private void ResetSelectItem_Click(object sender, RoutedEventArgs e)
        {
            //清空
            vm.SelectFruit = null;
        }
    }
}
