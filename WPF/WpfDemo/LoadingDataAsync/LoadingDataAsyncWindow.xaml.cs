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
using WpfDemo.LoadingDataAsync.Models;

namespace WpfDemo.LoadingDataAsync
{
    /// <summary>
    /// Interaction logic for LoadingDataAsyncWindow.xaml
    /// </summary>
    public partial class LoadingDataAsyncWindow : Window
    {
        ObservableCollection<Doctor> students;
        Random random;
        List<string> firstNameList;
        List<string> lastNameList;
        List<string> sexList;
        int order;
        public LoadingDataAsyncWindow()
        {
            InitializeComponent();
            InitializeData();
            //给DataContextAndItemSource的DataContext赋值
            DataContext = students;
        }

        private void InitializeData()
        {
            order = 1;
            random = new Random();
            firstNameList = new List<string>() { "雅萍", "雨婷", "雯媛", "茹雪", "熠彤", "鹏涛", "修杰", "伟泽", "昊然" };
            lastNameList = new List<string>() { "刘", "李", "程", "陈", "曾", "王", "梁", "吴" };
            sexList = new List<string>() { "男", "女", "未知" };
            GenerateData(20);
        }

        private void GenerateData(int addDataCount=5)
        {
            //这里根据业务改为查询api即可
            if (students == null)
            {
                students = new ObservableCollection<Doctor>();
            }
            if (order < 200)
            {
                for (int i = 0; i < addDataCount; i++)
                {
                    students.Add(new Doctor()
                    {
                        Order = order,
                        Age = random.Next(0, 50),
                        Name = $"{lastNameList[random.Next(0, lastNameList.Count)]}{firstNameList[random.Next(0, firstNameList.Count)]}",
                        Sex = sexList[random.Next(0, sexList.Count)]
                    });
                    order++;
                }
            }
            
           
        }

        private void GenerateData_Click(object sender, RoutedEventArgs e)
        {
            GenerateData();
        }

        private void DataGrid_ScrollChanged(object sender, ScrollChangedEventArgs e)
        {
            var scrollViewer = e.OriginalSource as ScrollViewer;
            if (e.VerticalOffset!=0 && e.VerticalOffset == scrollViewer.ScrollableHeight)
            {
                GenerateData();
            }
        }
    }
}
