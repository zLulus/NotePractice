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
    /// SingleSelectorWindow3.xaml 的交互逻辑
    /// </summary>
    public partial class SingleSelectorWindow3 : Window
    {
        public SingleSelectorWindow3()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 窗体加载时激发
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            TvTestDataBind();
        }

        /// <summary>
        /// 数据绑定
        /// </summary>
        private void TvTestDataBind()
        {
            IList<SingleSelector2ViewModel> treeList = new List<SingleSelector2ViewModel>();
            for (int i = 0; i < 5; i++)
            {
                SingleSelector2ViewModel tree = new SingleSelector2ViewModel();
                tree.Id = i.ToString();
                tree.Name = "Test" + i;
                tree.IsExpanded = true;
                for (int j = 0; j < 5; j++)
                {
                    SingleSelector2ViewModel child = new SingleSelector2ViewModel();
                    child.Id = i + "-" + j;
                    child.Name = "Test" + child.Id;
                    child.Parent = tree;
                    if (j % 2 == 0)
                    {
                        SingleSelector2ViewModel childsChild = new SingleSelector2ViewModel();
                        childsChild.Id = i + "-" + j + "-" + j;
                        childsChild.Name = "Test" + childsChild.Id;
                        childsChild.Parent = child;
                        child.Children.Add(childsChild);
                    }
                    tree.Children.Add(child);
                }
                treeList.Add(tree);
            }

            //设置数据
            singleSelector.ItemsSourceData = treeList;

        }

        /// <summary>
        /// 获得选择项集合
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSelectId_Click(object sender, RoutedEventArgs e)
        {
            IList<SingleSelector2ViewModel> treeList = singleSelector.CheckedItemsIgnoreRelation();

            MessageBox.Show(GetIds(treeList));

        }

        private string GetIds(IList<SingleSelector2ViewModel> treeList)
        {
            StringBuilder ids = new StringBuilder();

            foreach (SingleSelector2ViewModel tree in treeList)
            {
                ids.Append(tree.Id).Append(",");
            }
            return ids.ToString();
        }
    }
}
