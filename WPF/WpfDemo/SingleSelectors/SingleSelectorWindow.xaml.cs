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

namespace WpfDemo.SingleSelectors
{
    /// <summary>
    /// SingleSelectorWindow.xaml 的交互逻辑
    /// </summary>
    public partial class SingleSelectorWindow : Window
    {
        public SingleSelectorWindow()
        {
            InitializeComponent();
        }

        private void btnSelectId_Click(object sender, RoutedEventArgs e)
        {
            var sel = singleSelector.GetSelectItem();
            if (sel != null)
            {
                MessageBox.Show($"选中项：{sel.ToString()}");
            }
            else
            {
                MessageBox.Show($"没有选中项");
            }
        }
    }
}
