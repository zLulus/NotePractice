using Newtonsoft.Json;
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

namespace WpfDemo.NavigationPages
{
    /// <summary>
    /// NavigationPage.xaml 的交互逻辑
    /// </summary>
    public partial class NavigationPage : UserControl
    {
        private List<NavigationPageContentInfo> navigationPageContentInfos;
        private int currentOrder;
        /// <summary>
        /// 总数据源
        /// </summary>
        public object PagesDataContext;
        public NavigationPage(List<NavigationPageContentInfo> navigationPageContentInfos, object PagesDataContext)
        {
            InitializeComponent();

            //这些功能可以挪到NavigationPage的VM中（如果有）
            //Click事件都可以改成Command
            this.PagesDataContext = PagesDataContext;
            this.navigationPageContentInfos = navigationPageContentInfos.OrderBy(x => x.Order).ToList();
            currentOrder = 0;
            ChangePage();
        }

        private void ChangePage()
        {
            Pages.Content = this.navigationPageContentInfos[currentOrder].Content;
            navigationPageContentInfos[currentOrder].Content.DataContext = this.PagesDataContext;
        }

        private void PrePage_Click(object sender, RoutedEventArgs e)
        {
            if (currentOrder > 0)
                currentOrder--;
            else
                return;
            ChangePage();
        }

        private void NextPage_Click(object sender, RoutedEventArgs e)
        {
            if (currentOrder != navigationPageContentInfos.Count - 1)
                currentOrder++;
            else
                return;
            ChangePage();
        }

        private void GetDataContext_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(JsonConvert.SerializeObject(PagesDataContext));
        }
    }
}
