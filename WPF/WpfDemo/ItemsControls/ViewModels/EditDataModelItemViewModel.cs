using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WpfDemo.ItemsControls.ViewModels
{
    public class EditDataModelItemViewModel: BaseNotifyPropertyChanged
    {
        public ObservableCollection<EnumInfo> FillTypeEnumInfos { get; set; }
        public ObservableCollection<EnumInfo> IsUniqueKeyEnumInfos { get; set; }
        public bool IsCanModify { get; set; }
        private string tableChineseName;
        public string TableChineseName
        {
            get
            {
                return tableChineseName;
            }
            set
            {
                if (tableChineseName != value)
                {
                    tableChineseName = value;
                    NotifyPropertyChanged(nameof(TableChineseName));
                }
            }
        }
        public string FieldChineseName { get; set; }
        
        public bool IsCanFillTypeChange { get; set; }
        private EnumInfo fillTypeSelectItem;
        public EnumInfo FillTypeSelectItem
        {
            get
            {
                return fillTypeSelectItem;
            }
            set
            {
                if (fillTypeSelectItem != value)
                {
                    fillTypeSelectItem = value;
                    NotifyPropertyChanged(nameof(FillTypeSelectItem));
                }
            }
        }
        public bool IsUniqueKey { get; set; }
        private bool isCanIsUniqueKeyChange;
        public bool IsCanIsUniqueKeyChange
        {
            get
            {
                return isCanIsUniqueKeyChange;
            }
            set
            {
                if (isCanIsUniqueKeyChange != value)
                {
                    isCanIsUniqueKeyChange = value;
                    //查看模型
                    if (isCanIsUniqueKeyChange && !IsCanModify)
                    {
                        isCanIsUniqueKeyChange = false;
                    }
                    NotifyPropertyChanged(nameof(IsCanIsUniqueKeyChange));
                }
            }
        }
        private EnumInfo isUniqueKeySelectItem;
        public EnumInfo IsUniqueKeySelectItem
        {
            get
            {
                return isUniqueKeySelectItem;
            }
            set
            {
                if (isUniqueKeySelectItem != value)
                {
                    isUniqueKeySelectItem = value;
                    NotifyPropertyChanged(nameof(IsUniqueKeySelectItem));
                }
            }
        }

        private string specifiedValue;
        public string SpecifiedValue
        {
            get
            {
                return specifiedValue;
            }
            set
            {
                if (specifiedValue != value)
                {
                    specifiedValue = value;
                    NotifyPropertyChanged(nameof(SpecifiedValue));
                }
            }
        }
        private Visibility isSpecifiedValueVisibility;
        public Visibility IsSpecifiedValueVisibility
        {
            get
            {
                return isSpecifiedValueVisibility;
            }
            set
            {
                if (isSpecifiedValueVisibility != value)
                {
                    isSpecifiedValueVisibility = value;
                    NotifyPropertyChanged(nameof(IsSpecifiedValueVisibility));
                }
            }
        }

        private short minCount;
        public short MinCount
        {
            get
            {
                return minCount;
            }
            set
            {
                if (minCount != value)
                {
                    minCount = value;
                    NotifyPropertyChanged(nameof(MinCount));
                }
            }
        }

        private short maxCount;
        public short MaxCount
        {
            get
            {
                return maxCount;
            }
            set
            {
                if (maxCount != value)
                {
                    maxCount = value;
                    NotifyPropertyChanged(nameof(MaxCount));
                }
            }
        }
        private Visibility countVisibility;
        public Visibility CountVisibility
        {
            get
            {
                return countVisibility;
            }
            set
            {
                if (countVisibility != value)
                {
                    countVisibility = value;
                    NotifyPropertyChanged(nameof(CountVisibility));
                }
            }
        }

        private int minSize;
        public int MinSize
        {
            get
            {
                return minSize;
            }
            set
            {
                if (minSize != value)
                {
                    minSize = value;
                    NotifyPropertyChanged(nameof(MinSize));
                }
            }
        }
        private int maxSize;
        public int MaxSize
        {
            get
            {
                return maxSize;
            }
            set
            {
                if (maxSize != value)
                {
                    maxSize = value;
                    NotifyPropertyChanged(nameof(MaxSize));
                }
            }
        }
        private Visibility sizeVisibility;
        public Visibility SizeVisibility
        {
            get
            {
                return sizeVisibility;
            }
            set
            {
                if (sizeVisibility != value)
                {
                    sizeVisibility = value;
                    NotifyPropertyChanged(nameof(SizeVisibility));
                }
            }
        }
    }
}
