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
using WpfDemo.ListBoxShowImages.ViewModels;

namespace WpfDemo.ListBoxShowImages
{
    /// <summary>
    /// Interaction logic for ListBoxShowImagesDemo.xaml
    /// </summary>
    public partial class ListBoxShowImagesDemo : UserControl
    {
        ObservableCollection<ListBoxShowImagesViewModel> vm { get; set; }
        public ListBoxShowImagesDemo()
        {
            InitializeComponent();
        }

        private void imageListBox_Loaded(object sender, RoutedEventArgs e)
        {
            vm = new ObservableCollection<ListBoxShowImagesViewModel>();
            for(int i = 0; i < 100; i++)
            {
                if (i % 2 == 0)
                {
                    vm.Add(new ListBoxShowImagesViewModel() { Path = "/ListBoxShowImages/Images/OIP (1).jpg", Name = i.ToString() });
                }
                else
                {
                    vm.Add(new ListBoxShowImagesViewModel() { Path = "/ListBoxShowImages/Images/OIP.jpg", Name = i.ToString() });
                }
            }
            imageListBox.DataContext = vm;
        }
    }
}
