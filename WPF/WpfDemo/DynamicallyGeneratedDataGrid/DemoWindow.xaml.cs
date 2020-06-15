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
using WpfDemo.DynamicallyGeneratedDataGrid.Samples;
using WpfDemo.DynamicallyGeneratedDataGrid.Bases;
using Newtonsoft.Json;
using WpfDemo.DynamicallyGeneratedDataGrid.Samples.Converters;

namespace WpfDemo.DynamicallyGeneratedDataGrid
{
    /// <summary>
    /// DemoWindow.xaml 的交互逻辑
    /// </summary>
    public partial class DemoWindow : Window
    {
        DemoViewModel demoViewModel;
        public DemoWindow()
        {
            InitializeComponent();

            //设置表格内容
            table.Loaded += Table_Loaded;
            //设置数据源、分页事件
            demoViewModel = new DemoViewModel(10, 1);
            table.DataContext = demoViewModel;
        }

        private void Table_Loaded(object sender, RoutedEventArgs e)
        {
            //设置数据列
            List<SetDataColumnsItem> columnsItems = new List<SetDataColumnsItem>();
            columnsItems.Add(new SetDataColumnsItem() { Header = "Id", BindPath = "Id", DataGridLengthValue = 1, DataGridLengthUnitType = DataGridLengthUnitType.Star, Order = 1 });
            columnsItems.Add(new SetDataColumnsItem() { Header = "Id转换列", BindPath = "Id", DataGridLengthValue = 1, DataGridLengthUnitType = DataGridLengthUnitType.Star, Order = 1, DisplayEvent = new DisplayIdConverter() });
            columnsItems.Add(new SetDataColumnsItem() { Header = "ItemName", BindPath = "ItemName", DataGridLengthValue = 3, DataGridLengthUnitType = DataGridLengthUnitType.Star, Order = 2 });
            //设置操作列
            List<OperationInfo> operationInfos = new List<OperationInfo>();
            operationInfos.Add(new OperationInfo() { Content = "修改", ExecuteEvent = Modify_Click, CanExecuteEvent = new ModifyVisibilityConverter(), Order = 1 });
            operationInfos.Add(new OperationInfo() { Content = "删除", ExecuteEvent = Delete_Click, CanExecuteEvent = new DeleteVisibilityConverter(), Order = 2 });

            table.SetDataGrid(columnsItems, true, operationInfos, true);

        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Modify_Click(object sender, RoutedEventArgs e)
        {
            //选择项
            var o = table.MyDataGrid.SelectedItem;

        }

        private void GetSelectList_Click(object sender, RoutedEventArgs e)
        {
            var o = table.GetCheckedDataList();
            MessageBox.Show(JsonConvert.SerializeObject(o));
        }
    }
}
