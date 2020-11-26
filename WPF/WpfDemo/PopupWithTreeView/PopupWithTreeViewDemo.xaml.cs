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

namespace WpfDemo.PopupWithTreeView
{
    /// <summary>
    /// Interaction logic for PopupWithTreeViewDemo.xaml
    /// </summary>
    public partial class PopupWithTreeViewDemo : UserControl
    {
        public PopupWithTreeViewDemo()
        {
            InitializeComponent();
        }

        private void singleTree_Initialized(object sender, EventArgs e)
        {
            var trv = sender as TreeView;
            var trvItem = new TreeViewItem() { Header = "Initialized item" };
            var trvItemSel = trv.Items[1] as TreeViewItem;
            trvItemSel.Items.Add(trvItem);
        }

        private void singleTree_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            var trv = sender as TreeView;
            var trvItem = trv.SelectedItem as TreeViewItem;
            if (trvItem.Items.Count != 0) return;
            singleHeader.Text = trvItem.Header.ToString();
            singlePopup.IsOpen = false;
        }

        private void singleHeader_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            singlePopup.Placement = System.Windows.Controls.Primitives.PlacementMode.RelativePoint;
            singlePopup.VerticalOffset = singleHeader.Height;
            singlePopup.StaysOpen = true;
            singlePopup.Height = singleTree.Height;
            singlePopup.Width = singleHeader.Width;
            singlePopup.IsOpen = true;
        }
    }
}
