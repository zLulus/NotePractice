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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfDemo.ListBoxWithScroll
{
    /// <summary>
    /// Interaction logic for ListBoxWithScrollDemo.xaml
    /// </summary>
    public partial class ListBoxWithScrollDemo : UserControl
    {
        public ListBoxWithScrollDemo()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            for(int i = 0; i <= 500; i++)
            {
                listBox.Items.Add(new ListBoxItem() { Content = i });
            }
        }

        private void MoveToButtom_Click(object sender, RoutedEventArgs e)
        {
            if (listBox.Items.Count > 0)
            {
                var last = listBox.Items[listBox.Items.Count - 1];
                listBox.ScrollIntoView(last);
            }
            
        }
    }
}
