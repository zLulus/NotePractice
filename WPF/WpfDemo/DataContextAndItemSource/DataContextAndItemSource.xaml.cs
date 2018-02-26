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
using WpfDemo.DataContextAndItemSource.Models;

namespace WpfDemo.DataContextAndItemSource
{
    /// <summary>
    /// DataContextAndItemSource.xaml 的交互逻辑
    /// </summary>
    public partial class DataContextAndItemSource : Window
    {
        public DataContextAndItemSource()
        {
            InitializeComponent();
            ObservableCollection<Student> students = new ObservableCollection<Student>();
            students.Add(new Student()
            {
                Age = 15,
                Name = "吴芳",
                Sex = "女"
            });
            students.Add(new Student()
            {
                Age = 16,
                Name = "刘洋",
                Sex = "男"
            });
            //给DataContextAndItemSource的DataContext赋值
            DataContext = students;
        }
    }
}
