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
using WpfDemo.DiaplayRowNumberInListView.Models;

namespace WpfDemo.DiaplayRowNumberInListView
{
    /// <summary>
    /// Interaction logic for DiaplayRowNumberInListViewDemo2.xaml
    /// </summary>
    public partial class DiaplayRowNumberInListViewDemo2 : UserControl
    {
        public DiaplayRowNumberInListViewDemo2()
        {
            InitializeComponent();

            var datas = new ObservableCollection<DataModel>();
            datas.Add(new DataModel() { Name = "Jenny" });
            datas.Add(new DataModel() { Name = "Bill" });
            datas.Add(new DataModel() { Name = "Blue" });
            datas.Add(new DataModel() { Name = "blablaName" });
            listviewNames.DataContext = datas;
        }

        private void MoveUp_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            if (button == null)
                return;
            DataModel data = button.CommandParameter as DataModel;
            if (data == null)
                return;
            ObservableCollection<DataModel> datas = listviewNames.DataContext as ObservableCollection<DataModel>;
            if (datas == null)
                return;
            int index = datas.IndexOf(data);
            var newIndex = index > 0 ? index - 1 : index;
            datas.Move(index, newIndex);

            //int totalCount = datas.Count;
            //for (int i = 0; i < totalCount; i++)
            //{
            //    datas.Move(i, i);
            //}
        }

        private void MoveDown_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            if (button == null)
                return;
            DataModel data = button.CommandParameter as DataModel;
            if (data == null)
                return;
            ObservableCollection<DataModel> datas = listviewNames.DataContext as ObservableCollection<DataModel>;
            if (datas == null)
                return;
            int index = datas.IndexOf(data);
            var newIndex = index < datas.Count - 1 ? index + 1 : index;
            datas.Move(index, newIndex);

            //int totalCount = datas.Count;
            //for (int i = 0; i < totalCount; i++)
            //{
            //    datas.Move(i, i);
            //}
        }


    }
}
