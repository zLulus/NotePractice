using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using WpfDemo.DiaplayRowNumberInListView.Models;

namespace WpfDemo.DiaplayRowNumberInListView
{
    /// <summary>
    /// Interaction logic for DiaplayRowNumberInListViewDemo.xaml
    /// </summary>
    public partial class DiaplayRowNumberInListViewDemo : UserControl
    {
        public DiaplayRowNumberInListViewDemo()
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
            ObservableCollection<DataModel> behaviors = listviewNames.DataContext as ObservableCollection<DataModel>;
            if (behaviors == null)
                return;
            int index = behaviors.IndexOf(data);
            var newIndex = index > 0 ? index - 1 : index;
            behaviors.Move(index, newIndex);
        }

        private void MoveDown_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            if (button == null)
                return;
            DataModel data = button.CommandParameter as DataModel;
            if (data == null)
                return;
            ObservableCollection<DataModel> behaviors = listviewNames.DataContext as ObservableCollection<DataModel>;
            if (behaviors == null)
                return;
            int index = behaviors.IndexOf(data);
            var newIndex = index < behaviors.Count - 1 ? index + 1 : index;
            behaviors.Move(index, newIndex);
        }
    }


}
