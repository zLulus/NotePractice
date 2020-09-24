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
using WpfDemo.Bind.Models;

namespace WpfDemo.Bind
{
    /// <summary>
    /// Interaction logic for DataGridBindMultiData.xaml
    /// </summary>
    public partial class DataGridBindMultiData : UserControl
    {
        ObservableCollection<Teacher> teachers;
        public DataGridBindMultiData()
        {
            InitializeComponent();
            //这里没有涉及双向绑定，即INotifyPropertyChanged的内容
            SetData();
            DataContext = teachers;
        }

        private void SetData()
        {
            teachers = new ObservableCollection<Teacher>();
            teachers.Add(new Teacher()
            {
                SchoolNumber = "001",
                Name = "李胜",
                Sex = "女",
                TeacherDetailInfo = new TeacherDetailInfo()
                {
                    EntryTime = new DateTime(2018,1,1),
                    Address = "地址1",
                }
            });
            teachers.Add(new Teacher()
            {
                SchoolNumber = "002",
                Name = "杨军",
                Sex = "男",
                TeacherDetailInfo = new TeacherDetailInfo()
                {
                    EntryTime = new DateTime(2018, 2, 10),
                    Address = "地址2",
                }
            });
            teachers.Add(new Teacher()
            {
                SchoolNumber = "003",
                Name = "李理",
                Sex = "男",
                TeacherDetailInfo = new TeacherDetailInfo()
                {
                    EntryTime = new DateTime(2018, 3, 20),
                    Address = "地址3",
                }
            });
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            var num = (sender as Button).Tag.ToString();
            var item= teachers.FirstOrDefault(x => x.SchoolNumber == num);
            if (item != null)
            {
                teachers.Remove(item);
            }
        }
    }
}
