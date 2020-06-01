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
using WpfDemo.SingleSelectors.ViewModels;

namespace WpfDemo.SingleSelectors
{
    /// <summary>
    /// SingleSelector.xaml 的交互逻辑
    /// </summary>
    public partial class SingleSelector : UserControl
    {
        string groupName = "MyGroup";
        public SingleSelector()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 动态加载数据，默认所有radioButton只想选择一个
        /// </summary>
        /// <param name="data"></param>
        public SingleSelector(List<SingleSelectorViewModel> data)
        {
            InitializeComponent();
            tree.Items.Clear();
            //后端加载数据
            foreach (var item in data)
            {
                //非子节点
                if (item.Chidlren != null && item.Chidlren.Count != 0)
                {
                    TreeViewItem child = new TreeViewItem() { Header = item.Name, Tag = item };
                    tree.Items.Add(child);
                    SetTreeView(child, item.Chidlren);
                }
                //子节点
                else
                {
                    tree.Items.Add(new System.Windows.Controls.RadioButton() { GroupName = groupName, Content = item.Name, Tag = item });
                }
            }
        }

        private void SetTreeView(TreeViewItem treeView, List<SingleSelectorViewModel> chidlren)
        {
            foreach (var item in chidlren)
            {
                if (item.Chidlren != null && item.Chidlren.Count != 0)
                {
                    TreeViewItem child = new TreeViewItem() { Header = item.Name, Tag = item };
                    treeView.Items.Add(child);
                    SetTreeView(child, item.Chidlren);
                }
                else
                {
                    treeView.Items.Add(new System.Windows.Controls.RadioButton() { GroupName = groupName, Content = item.Name, Tag = item });
                }
            }
        }

        public object GetSelectItem()
        {
            object sel = null;
            TraverseChildren(tree.Items, ref sel);
            return sel;
        }

        private void TraverseChildren(ItemCollection items, ref object sel)
        {
            foreach (var child in items)
            {
                if (child is System.Windows.Controls.RadioButton)
                {
                    var radioButton = child as System.Windows.Controls.RadioButton;
                    if (radioButton.IsChecked.HasValue && radioButton.IsChecked.Value)
                    {
                        sel = radioButton.Tag;
                        break;
                    }
                }
                else
                {
                    var treeViewItem = child as TreeViewItem;
                    TraverseChildren(treeViewItem.Items, ref sel);
                }
            }
        }
    }
}
