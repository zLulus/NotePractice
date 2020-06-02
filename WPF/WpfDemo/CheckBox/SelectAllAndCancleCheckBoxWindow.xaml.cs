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
using WpfDemo.CheckBox.ViewModels;

namespace WpfDemo.CheckBox
{
    /// <summary>
    /// SelectAllAndCancleCheckBoxWindow.xaml 的交互逻辑
    /// </summary>
    public partial class SelectAllAndCancleCheckBoxWindow : Window
    {
        public SelectAllAndCancleCheckBoxWindow()
        {
            InitializeComponent();

            List<SelectAllAndCancleCheckBoxViewModel> data = new List<SelectAllAndCancleCheckBoxViewModel>();
            for(int i = 0; i < 10; i++)
            {
                data.Add(new SelectAllAndCancleCheckBoxViewModel() { Name = $"No. {i.ToString()}" });
            }
            
            DataContext = data;
        }
    }
}
