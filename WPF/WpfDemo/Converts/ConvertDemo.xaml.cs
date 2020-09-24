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
using WpfDemo.Converts.Models;

namespace WpfDemo.Converts
{
    /// <summary>
    /// ConvertDemo.xaml 的交互逻辑
    /// </summary>
    public partial class ConvertDemo : UserControl
    {
        public ConvertDemo()
        {
            InitializeComponent();
            ObservableCollection<Student> students = new ObservableCollection<Student>();
            students.Add(new Student()
            {
                Name = "1",
                Age = 22
            });
            students.Add(new Student()
            {
                Name = "2",
                Age = 21
            });
            students.Add(new Student()
            {
                Name = "3",
                Age = 20
            });
            students.Add(new Student()
            {
                Name = "4",
                Age = 18
            });
            students.Add(new Student()
            {
                Name = "5",
                Age = 19
            });
            students.Add(new Student()
            {
                Name = "6",
                Age = 22
            });
            DataContext = students;
        }
    }
}
