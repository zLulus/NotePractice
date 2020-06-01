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

namespace WpfDemo.MultiSelectors
{
    /// <summary>
    /// MultiSelectorWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MultiSelectorWindow : Window
    {
        public MultiSelectorWindow()
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
            IList<MultiSelectorViewModel> treeList = new List<MultiSelectorViewModel>();
            for (int i = 0; i < 5; i++)
            {
                MultiSelectorViewModel tree = new MultiSelectorViewModel();
                tree.Id = i.ToString();
                tree.Name = "Test" + i;
                tree.IsExpanded = true;
                for (int j = 0; j < 5; j++)
                {
                    MultiSelectorViewModel child = new MultiSelectorViewModel();
                    child.Id = i + "-" + j;
                    child.Name = "Test" + child.Id;
                    child.Parent = tree;
                    if (j % 2 == 0)
                    {
                        MultiSelectorViewModel childsChild = new MultiSelectorViewModel();
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
            multiSelector.ItemsSourceData = treeList;

        }

        /// <summary>
        /// 获得选择项集合
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSelectId_Click(object sender, RoutedEventArgs e)
        {
            IList<MultiSelectorViewModel> treeList = multiSelector.CheckedItemsIgnoreRelation();

            MessageBox.Show(GetIds(treeList));

        }

        private string GetIds(IList<MultiSelectorViewModel> treeList)
        {
            StringBuilder ids = new StringBuilder();

            foreach (MultiSelectorViewModel tree in treeList)
            {
                ids.Append(tree.Id).Append(",");
            }
            return ids.ToString();
        }
    }
}
