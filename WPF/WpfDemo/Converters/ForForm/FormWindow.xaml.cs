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

namespace WpfDemo.Converters.ForForm
{
    /// <summary>
    /// FormWindow.xaml 的交互逻辑
    /// </summary>
    public partial class FormWindow : UserControl
    {
        EditFieldMetaViewModel vm;
        public FormWindow()
        {
            InitializeComponent();

            vm = new EditFieldMetaViewModel();
            vm.FieldTypeEnumInfos = new ObservableCollection<EnumInfo>();
            var list = EnumTool.GetEnumList<FieldTypeEnum>();
            list.ForEach(x => vm.FieldTypeEnumInfos.Add(x));
            DataContext = vm;
        }
    }
}
