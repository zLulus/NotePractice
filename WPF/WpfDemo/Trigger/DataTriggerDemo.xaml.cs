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
using System.Windows.Shapes;
using WpfDemo.Trigger.Models;

namespace WpfDemo.Trigger
{
    /// <summary>
    /// DataTriggerDemo.xaml 的交互逻辑
    /// </summary>
    public partial class DataTriggerDemo : Window
    {
        public DataTriggerDemo()
        {
            InitializeComponent();

            ObservableCollection<TypeClass> types=new ObservableCollection<TypeClass>();
            types.Add(new TypeClass()
            {
                TypeValue = "1",
                Type = 1
            });
            types.Add(new TypeClass()
            {
                TypeValue = "0",
                Type = 0
            });
            types.Add(new TypeClass()
            {
                TypeValue = "2",
                Type = 2
            });
            types.Add(new TypeClass()
            {
                TypeValue = "1",
                Type = 1
            });
            types.Add(new TypeClass()
            {
                TypeValue = "2",
                Type = 2
            });
            types.Add(new TypeClass()
            {
                TypeValue = "0",
                Type = 0
            });
            DataContext = types;
        }
    }
}
