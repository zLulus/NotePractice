using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfDemo.VirtualizingListBox
{
    /// <summary>
    /// VirtualizingListBoxDemo.xaml 的交互逻辑
    /// </summary>
    public partial class VirtualizingListBoxDemo : UserControl
    {
        Timer timer;
        int number = 1;
        public VirtualizingListBoxDemo()
        {
            InitializeComponent();

            timer=new Timer();
            timer.Interval = 1;
            timer.Elapsed += AddData;
            listBox.ItemsSource = new ObservableCollection<string>();
        }

        private void AddData(object sender, ElapsedEventArgs e)
        {
            listBox.Dispatcher.Invoke(() =>
            {
                number++;
                var dataContext = listBox.ItemsSource as ObservableCollection<string>;
                if (dataContext != null)
                    dataContext.Add(number.ToString());
                if (listBox.Items.Count > 0)
                {
                    listBox.ScrollIntoView(listBox.Items[listBox.Items.Count - 1]);
                }
                if (dataContext != null && dataContext.Count > 200)
                {
                    dataContext.Clear();
                }

            });
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            timer.Start();
        }
    }
}
