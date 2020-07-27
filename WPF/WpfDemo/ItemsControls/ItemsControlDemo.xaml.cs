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
using System.Windows.Navigation;
using System.Windows.Shapes;
using WpfDemo.ItemsControls.ViewModels;

namespace WpfDemo.ItemsControls
{
    /// <summary>
    /// ItemsControlDemo.xaml 的交互逻辑
    /// </summary>
    public partial class ItemsControlDemo : UserControl
    {
        ObservableCollection<EditDataModelOneTableViewModel> DataModelTabls { get; set; }
        public ItemsControlDemo()
        {
            InitializeComponent();

            DataModelTabls = new ObservableCollection<EditDataModelOneTableViewModel>();
            //枚举应该从枚举类中反射获取
            var fillTypeEnumList = new ObservableCollection<EnumInfo>();
            fillTypeEnumList.Add(new EnumInfo() { Description = "枚举值1", Value = 1, Name = "Enum1" });
            fillTypeEnumList.Add(new EnumInfo() { Description = "枚举值2", Value = 2, Name = "Enum2" });
            var isUniqueKeyEnumList=new ObservableCollection<EnumInfo>();
            isUniqueKeyEnumList.Add(new EnumInfo() { Description = "是", Value = 1, Name = "Yes" });
            isUniqueKeyEnumList.Add(new EnumInfo() { Description = "否", Value = 2, Name = "No" });
            for (int i = 0; i < 3; i++)
            {
                bool isFirst = i == 0 ? true : false;
                EditDataModelOneTableViewModel oneTable = new EditDataModelOneTableViewModel()
                {
                    DataModelEdition = new EditDataModelEditionViewModel()
                    {
                        EditionId = i,
                        MainTableChineseName = "Test1",
                        TableChineseName = $"Test{i}",
                        IsCanModify = !isFirst,
                        OneToOneIsChecked = true,
                    },
                    DataModelItems = new ObservableCollection<EditDataModelItemViewModel>()
                };
                if (isFirst)
                {
                    oneTable.DataModelEdition.OneToOneIsChecked = true;
                }
                for(int j = 0; j < 10; j++)
                {
                    var item = new EditDataModelItemViewModel()
                    {
                        FillTypeEnumInfos = fillTypeEnumList,
                        IsUniqueKeyEnumInfos = isUniqueKeyEnumList,
                        IsCanModify = !isFirst,
                        TableChineseName = $"Test{i}",
                        FieldChineseName = $"Filed{j}",
                        IsCanFillTypeChange = !isFirst,
                        IsUniqueKey = !isFirst,
                        IsCanIsUniqueKeyChange = !isFirst,
                        IsSpecifiedValueVisibility = Visibility.Visible,
                        CountVisibility = isFirst == true ? Visibility.Collapsed : Visibility.Visible,
                        SizeVisibility = isFirst == true ? Visibility.Collapsed : Visibility.Visible
                    };
                    if (isFirst)
                    {
                        item.FillTypeSelectItem = fillTypeEnumList[0];
                        item.IsUniqueKeySelectItem = isUniqueKeyEnumList[0];
                        item.SpecifiedValue = "111";
                    }
                    oneTable.DataModelItems.Add(item);
                }
               
                DataModelTabls.Add(oneTable);
            }
            
            listView.DataContext = DataModelTabls;
        }
    }
}
