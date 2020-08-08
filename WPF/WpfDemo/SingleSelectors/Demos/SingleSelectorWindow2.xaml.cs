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
using System.Windows.Shapes;
using WpfDemo.SingleSelectors.ViewModels;

namespace WpfDemo.SingleSelectors
{
    /// <summary>
    /// SingleSelectorWindow2.xaml 的交互逻辑
    /// </summary>
    public partial class SingleSelectorWindow2 : UserControl
    {
        SingleSelector singleSelector;
        public SingleSelectorWindow2()
        {
            InitializeComponent();

            //模拟数据
            List<SingleSelectorViewModel> data = new List<SingleSelectorViewModel>();
            var first = new SingleSelectorViewModel() { Id = 1, ParendId = 0, Name = "湖南省" };
            first.Children = new List<SingleSelectorViewModel>();
            var child1 = new SingleSelectorViewModel() { Id = 11, ParendId = 1, Name = "长沙市" };
            child1.Children = new List<SingleSelectorViewModel>();
            child1.Children.Add(new SingleSelectorViewModel() { Id = 111, ParendId = 11, Name = "芙蓉区" });
            child1.Children.Add(new SingleSelectorViewModel() { Id = 112, ParendId = 11, Name = "天心区" });
            first.Children.Add(child1);
            first.Children.Add(new SingleSelectorViewModel() { Id = 12, ParendId = 1, Name = "株洲市" });
            data.Add(first);
            var second = new SingleSelectorViewModel() { Id = 2, ParendId = 0, Name = "北京市" };
            data.Add(second);

            //后台创建用户控件
            singleSelector = new SingleSelector(data);
            border.Child = singleSelector;

            //设置数据源
            singleSelector2.SetTree(data);
        }

        private void btnSelectId_Click(object sender, RoutedEventArgs e)
        {
            var sel = singleSelector.GetSelectItem();
            if (sel != null)
            {
                var d = sel as SingleSelectorViewModel;
                MessageBox.Show($"选中项：{d.Id},{d.Name}");
            }
            else
            {
                MessageBox.Show($"没有选中项");
            }
        }
    }
}
