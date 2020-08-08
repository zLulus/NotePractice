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
using WpfDemo.Template.DataTemplate.Models;

namespace WpfDemo.Template.DataTemplate
{
    /// <summary>
    /// DataTemplateDemo.xaml 的交互逻辑
    /// </summary>
    public partial class DataTemplateDemo : UserControl
    {
        public DataTemplateDemo()
        {
            InitializeComponent();

            ObservableCollection<Person> persons = new ObservableCollection<Person>();
            persons.Add(new Person()
            {
                Name = "吴南",
                Age = 20,
                Hometown = "成都"
            });
            persons.Add(new Person()
            {
                Name = "刘艳",
                Age = 21,
                Hometown = "北京"
            });
            persons.Add(new Person()
            {
                Name = "刘芸",
                Age = 19,
                Hometown = "上海"
            });
            DataContext = persons;
        }
    }
}
