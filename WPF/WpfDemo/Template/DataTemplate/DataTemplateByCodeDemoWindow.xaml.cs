using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Xml;
using WpfDemo.Template.DataTemplate.Models;

namespace WpfDemo.Template.DataTemplate
{
    /// <summary>
    /// DataTemplateByCodeDemoWindow.xaml 的交互逻辑
    /// </summary>
    public partial class DataTemplateByCodeDemoWindow : UserControl
    {
        public DataTemplateByCodeDemoWindow()
        {
            InitializeComponent();

            //设置一些数据
            var data = new List<Person>();
            for(int i = 0; i < 10; i++)
            {
                data.Add(new Person() { Name = $"haha{i}", Hometown = "HangZhou", Age = 21+i });
            }
            
            dataGrid1.DataContext = data;
            dataGrid2.DataContext = data;
        }

        private void GenerateByFrameworkElementFactory_Click(object sender, RoutedEventArgs e)
        {
            //FrameworkElementFactory 
            //https://stackoverflow.com/questions/1754608/what-is-the-code-behind-for-datagridtemplatecolumn-and-how-to-use-it
            //https://stackoverflow.com/questions/45036667/create-a-grid-in-wpf-with-a-frameworkelementfactory

            DataGridTemplateColumn dataGridTemplateColumn = new DataGridTemplateColumn();
            dataGridTemplateColumn.Header = "操作";
            dataGridTemplateColumn.Width = new DataGridLength(1, DataGridLengthUnitType.Star);
            //样式
            //dataGridTemplateColumn.CellStyle=new System.Windows.Style()
            //dataGridTemplateColumn.HeaderStyle=

            //Grid分列
            FrameworkElementFactory gridFactory = new FrameworkElementFactory(typeof(Grid));
            FrameworkElementFactory col1 = new FrameworkElementFactory(typeof(ColumnDefinition));
            FrameworkElementFactory col2 = new FrameworkElementFactory(typeof(ColumnDefinition));
            
            col1.SetValue(ColumnDefinition.WidthProperty, new GridLength(1, GridUnitType.Star));
            col2.SetValue(ColumnDefinition.WidthProperty, new GridLength(1, GridUnitType.Star));
            gridFactory.AppendChild(col1);
            gridFactory.AppendChild(col2);

            //添加两个Button
            FrameworkElementFactory btn1Factory = new FrameworkElementFactory(typeof(Button));
            btn1Factory.SetValue(Button.ContentProperty, "修改");
            //添加事件
            //https://stackoverflow.com/questions/47401286/how-to-pro-gramatically-add-click-event-to-frameworkelementfactory
            btn1Factory.AddHandler(Button.ClickEvent, new RoutedEventHandler(ModifyData_Click));
            FrameworkElementFactory btn2Factory = new FrameworkElementFactory(typeof(Button));
            btn2Factory.SetValue(Button.ContentProperty, "删除");
            btn2Factory.AddHandler(Button.ClickEvent, new RoutedEventHandler(DeleteData_Click));
            btn2Factory.SetValue(Grid.ColumnProperty, 1);

            gridFactory.AppendChild(btn1Factory);
            gridFactory.AppendChild(btn2Factory);
            //关键代码
            System.Windows.DataTemplate cellTemplate1 = new System.Windows.DataTemplate();
            cellTemplate1.VisualTree = gridFactory;
            dataGridTemplateColumn.CellTemplate = cellTemplate1;
            dataGrid1.Columns.Add(dataGridTemplateColumn);
        }

        private void DeleteData_Click(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void ModifyData_Click(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void GenerateByXmlReader_Click(object sender, RoutedEventArgs e)
        {
            //通过xml生成控件
            //xml无法添加事件，需要在完成控件添加后，查询到控件，添加事件方法
            //https://stackoverflow.com/questions/46522636/how-generate-custom-columns-for-frameworkelementfactorytypeofdatagrid

            var dataTemplateString =
                        @" <DataTemplate xmlns=""http://schemas.microsoft.com/winfx/2006/xaml/presentation"" 
                                        xmlns:x=""http://schemas.microsoft.com/winfx/2006/xaml"">
                            <StackPanel Orientation=""Horizontal"">
                                <Button Content=""修改"" x:Name=""mButton""></Button>
                                <Button Content=""删除""></Button>
                            </StackPanel>
                        </DataTemplate>";

            var stringReader = new StringReader(dataTemplateString);
            XmlReader xmlReader = XmlReader.Create(stringReader);
            System.Windows.DataTemplate dataTemplate = (System.Windows.DataTemplate)XamlReader.Load(xmlReader);
            DataGridTemplateColumn col1 = new DataGridTemplateColumn();
            col1.CellTemplate = dataTemplate;
            dataGrid2.Columns.Add(col1);
            //button 事件 查找控件加载
            //https://stackoverflow.com/questions/8666522/xamlreader-with-click-event
            //Find the DataRowView for DataGrid.
        }
    }
}
