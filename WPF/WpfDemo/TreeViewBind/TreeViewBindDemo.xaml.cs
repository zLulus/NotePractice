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
using WpfDemo.TreeViewBind.ViewModels;

namespace WpfDemo.TreeViewBind
{
    /// <summary>
    /// Interaction logic for TreeViewBindDemo.xaml
    /// </summary>
    public partial class TreeViewBindDemo : UserControl
    {
        public TreeViewBindDemo()
        {
            InitializeComponent();
            List<DisplayViewModel> vm = new List<DisplayViewModel>();
            DisplayViewModel oneData = new DisplayViewModel() { Name="1", Children=new List<DisplayViewModel>()};
            oneData.Children.Add(new DisplayViewModel() { Name = "1-1", Children = new List<DisplayViewModel>()
                {
                    new DisplayViewModel(){Name="1-1-1",Children=new List<DisplayViewModel>()}
                } 
            });
            oneData.Children.Add(new DisplayViewModel()
            {
                Name = "1-2",
                Children = new List<DisplayViewModel>()
                {
                    new DisplayViewModel(){Name="1-2-1",Children=new List<DisplayViewModel>()
                        {
                            new DisplayViewModel(){Name="1-2-1-2",Children=new List<DisplayViewModel>()}
                        } 
                    }
                }
            });
            vm.Add(oneData);
            vm.Add(new DisplayViewModel() { Name = "2", Children = new List<DisplayViewModel>() });
            treeView.ItemsSource = vm;
        }
    }
}
