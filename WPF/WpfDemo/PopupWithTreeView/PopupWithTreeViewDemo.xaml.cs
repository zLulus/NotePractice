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
using WpfDemo.PopupWithTreeView.ViewModels;

namespace WpfDemo.PopupWithTreeView
{
    /// <summary>
    /// Interaction logic for PopupWithTreeViewDemo.xaml
    /// </summary>
    public partial class PopupWithTreeViewDemo : UserControl
    {
        ObservableCollection<AdministrationViewModel> singleDataContext { get; set; }
        ObservableCollection<AdministrationViewModel> multiDataContext { get; set; }
        public PopupWithTreeViewDemo()
        {
            InitializeComponent();
        }

        private void singleTree_Initialized(object sender, EventArgs e)
        {
            singleDataContext = new ObservableCollection<AdministrationViewModel>();
            var beijing = new AdministrationViewModel() { Name = "北京市", Id = Guid.NewGuid().ToString() };
            beijing.Children.Add(new AdministrationViewModel() { Name = "朝阳区", Id = Guid.NewGuid().ToString() });
            beijing.Children.Add(new AdministrationViewModel() { Name = "海淀区", Id = Guid.NewGuid().ToString() });
            beijing.Children.Add(new AdministrationViewModel() { Name = "通州区", Id = Guid.NewGuid().ToString() });
            singleDataContext.Add(beijing);
            var guangdong = new AdministrationViewModel() { Name = "广东省", Id = Guid.NewGuid().ToString() };
            guangdong.Children.Add(new AdministrationViewModel() { Name = "汕尾市", Id = Guid.NewGuid().ToString() });
            guangdong.Children.Add(new AdministrationViewModel() { Name = "中山市", Id = Guid.NewGuid().ToString() });
            var guangzhou = new AdministrationViewModel() { Name = "广州市", Id = Guid.NewGuid().ToString() };
            guangzhou.Children.Add(new AdministrationViewModel() { Name = "越秀区", Id = Guid.NewGuid().ToString() });
            guangzhou.Children.Add(new AdministrationViewModel() { Name = "海珠区", Id = Guid.NewGuid().ToString() });
            guangzhou.Children.Add(new AdministrationViewModel() { Name = "番禺区", Id = Guid.NewGuid().ToString() });
            guangdong.Children.Add(guangzhou);
            singleDataContext.Add(guangdong);

            var trv = sender as TreeView;
            trv.DataContext = singleDataContext;
        }

        private void singleTree_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            var trv = sender as TreeView;
            var trvItem = trv.SelectedItem as AdministrationViewModel;
            //这里是否被选择的条件为是否为叶子结点，也可以使用AdministrationViewModel中的属性灵活控制
            if (trvItem.Children.Count != 0) return;
            singleHeader.Text = trvItem.Name.ToString();
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

        private void multiHeader_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            multiPopup.Placement = System.Windows.Controls.Primitives.PlacementMode.RelativePoint;
            multiPopup.VerticalOffset = multiHeader.Height;
            multiPopup.StaysOpen = true;
            multiPopup.Height = multiTree.Height;
            multiPopup.Width = multiHeader.Width;
            multiPopup.IsOpen = true;
        }

        private void multiTree_Initialized(object sender, EventArgs e)
        {
            multiDataContext = new ObservableCollection<AdministrationViewModel>();
            var beijing = new AdministrationViewModel() { Name = "北京市", Id = Guid.NewGuid().ToString() };
            beijing.Children.Add(new AdministrationViewModel() { Name = "朝阳区", Id = Guid.NewGuid().ToString(),IsCanChecked=true });
            beijing.Children.Add(new AdministrationViewModel() { Name = "海淀区", Id = Guid.NewGuid().ToString(), IsCanChecked = true });
            beijing.Children.Add(new AdministrationViewModel() { Name = "通州区", Id = Guid.NewGuid().ToString() });
            multiDataContext.Add(beijing);
            var guangdong = new AdministrationViewModel() { Name = "广东省", Id = Guid.NewGuid().ToString() };
            guangdong.Children.Add(new AdministrationViewModel() { Name = "汕尾市", Id = Guid.NewGuid().ToString(), IsCanChecked = true });
            guangdong.Children.Add(new AdministrationViewModel() { Name = "中山市", Id = Guid.NewGuid().ToString(), IsCanChecked = true });
            var guangzhou = new AdministrationViewModel() { Name = "广州市", Id = Guid.NewGuid().ToString() };
            guangzhou.Children.Add(new AdministrationViewModel() { Name = "越秀区", Id = Guid.NewGuid().ToString(), IsCanChecked = true });
            guangzhou.Children.Add(new AdministrationViewModel() { Name = "海珠区", Id = Guid.NewGuid().ToString(), IsCanChecked = true });
            guangzhou.Children.Add(new AdministrationViewModel() { Name = "番禺区", Id = Guid.NewGuid().ToString(), IsCanChecked = true });
            guangdong.Children.Add(guangzhou);
            multiDataContext.Add(guangdong);
            var trv = sender as TreeView;
            trv.DataContext = multiDataContext;
        }

        private static void GetSelectList(List<AdministrationViewModel> selectList, AdministrationViewModel item)
        {
            if(item.Children!=null)
            {
                foreach (var child in item.Children)
                {
                    if (child.IsCanChecked && child.IsChecked)
                        selectList.Add(child);
                    GetSelectList(selectList, child);
                }
            }
            
        }

        private void CheckBox_CheckedOrUncheck(object sender, RoutedEventArgs e)
        {
            var trv = multiTree;
            var data = trv.DataContext as ObservableCollection<AdministrationViewModel>;
            //获得所有被勾选的选项:这里使用IsChecked和IsCanChecked进行判断->可以根据业务改为其他的逻辑
            var selectList = new List<AdministrationViewModel>();
            foreach (var item in data)
            {
                if (item.IsCanChecked && item.IsChecked)
                    selectList.Add(item);
                GetSelectList(selectList, item);
            }
            var selectStr = "";
            foreach (var item in selectList)
            {
                selectStr += item.Name + ",";
            }
            selectStr = selectStr.TrimEnd(',');
            multiHeader.Text = selectStr;
        }

        private void CloseMultiPopup_Click(object sender, RoutedEventArgs e)
        {
            multiPopup.IsOpen = false;
        }
    }
}
