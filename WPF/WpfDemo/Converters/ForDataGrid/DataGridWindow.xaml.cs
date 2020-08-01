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
using System.Windows.Shapes;

namespace WpfDemo.Converters
{
    /// <summary>
    /// DataGridWindow.xaml 的交互逻辑
    /// </summary>
    public partial class DataGridWindow : UserControl
    {
        public DataGridWindow()
        {
            InitializeComponent();

            var FakeSource = FakeDatabase.GenerateFakeSource();
            dataGrid.DataContext = FakeSource;

            SetOperations();
        }

        /// <summary>
        /// 设置操作列
        /// </summary>
        /// <param name="input"></param>
        private void SetOperations()
        {
            DataGridTemplateColumn dataGridTemplateColumn = new DataGridTemplateColumn();
            dataGridTemplateColumn.Width = new DataGridLength(1, DataGridLengthUnitType.Star);
            dataGridTemplateColumn.Header = "操作";

            FrameworkElementFactory gridFactory = new FrameworkElementFactory(typeof(Grid));
            
            //修改按钮
            AddButton(gridFactory, 0, "修改", ModifyData, new ModifyVisibilityConverter());
            //删除按钮
            AddButton(gridFactory, 1, "删除", DeleteData, new DeleteVisibilityConverter());

            DataTemplate cellTemplate1 = new DataTemplate();
            cellTemplate1.VisualTree = gridFactory;
            dataGridTemplateColumn.CellTemplate = cellTemplate1;
            dataGrid.Columns.Add(dataGridTemplateColumn);
        }

        private static void AddButton(FrameworkElementFactory gridFactory, int i, string content, RoutedEventHandler routedEventHandler, IValueConverter valueConverter)
        {
            FrameworkElementFactory col1 = new FrameworkElementFactory(typeof(ColumnDefinition));
            col1.SetValue(ColumnDefinition.WidthProperty, new GridLength(1, GridUnitType.Star));
            gridFactory.AppendChild(col1);

            //https://stackoverflow.com/questions/47401286/how-to-pro-gramatically-add-click-event-to-frameworkelementfactory
            FrameworkElementFactory btn1Factory = new FrameworkElementFactory(typeof(Button));
            btn1Factory.SetValue(Button.ContentProperty, content);
            btn1Factory.AddHandler(Button.ClickEvent, routedEventHandler);
            //绑定数据源
            var bind = new Binding("DataContext");
            //绑定每一行的数据
            //https://stackoverflow.com/questions/39873228/how-to-bind-to-wpf-datagrid-row-class-instance-and-not-its-property
            bind.RelativeSource = new RelativeSource(RelativeSourceMode.FindAncestor) { AncestorType = typeof(DataGridRow) };
            //设置成自定义Converter
            bind.Converter = valueConverter;
            //判断该按钮是否应该存在
            btn1Factory.SetBinding(Button.VisibilityProperty, bind);

            btn1Factory.SetValue(Grid.ColumnProperty, i);
            gridFactory.AppendChild(btn1Factory);
        }

        private void ModifyData(object sender, RoutedEventArgs e)
        {

        }

        private void DeleteData(object sender, RoutedEventArgs e)
        {

        }
    }
}
