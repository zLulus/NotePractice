using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace WpfDemo.Converters.ForForm
{
    public class EditFieldMetaViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        //实现INotifyPropertyChanged接口
        internal void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public long ID { get; set; }
        /// <summary>
        /// 所属表ID
        /// </summary>
        public long TableMetaId { get; set; }
        /// <summary>
        /// 所属版本ID
        /// </summary>
        public long EditionId { get; set; }
        public bool IsCanDeleted { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsPrimaryKey { get; set; }
        public long CopyFrom { get; set; }
        /// <summary>
        /// 字段类型
        /// </summary>
        public ObservableCollection<EnumInfo> FieldTypeEnumInfos { get; set; }

        private string chineseName;
        /// <summary>
        /// 中文名
        /// </summary>
        public string ChineseName
        {
            get
            {
                return chineseName;
            }
            set
            {
                if (chineseName != value)
                {
                    chineseName = value;
                    NotifyPropertyChanged(nameof(ChineseName));
                }
            }
        }

        private string englishName;
        /// <summary>
        /// 英文名
        /// </summary>
        public string EnglishName
        {
            get
            {
                return englishName;
            }
            set
            {
                if (englishName != value)
                {
                    englishName = value;
                    NotifyPropertyChanged(nameof(EnglishName));
                }
            }
        }

        private FieldTypeEnum fieldType;
        /// <summary>
        /// 字段类型
        /// </summary>
        public FieldTypeEnum FieldType
        {
            get
            {
                return fieldType;
            }
            set
            {
                if (fieldType != value)
                {
                    fieldType = value;
                    NotifyPropertyChanged(nameof(FieldType));
                }
            }
        }

        private bool isUniqueKey;
        /// <summary>
        /// 是否唯一(字符类型/数字类型适用)
        /// </summary>
        public bool IsUniqueKey
        {
            get
            {
                return isUniqueKey;
            }
            set
            {
                if (isUniqueKey != value)
                {
                    isUniqueKey = value;
                    NotifyPropertyChanged(nameof(IsUniqueKey));
                }
            }
        }

        private bool isNullable;
        /// <summary>
        /// 是否可为空
        /// 若字段为唯一键，则不允许可为空
        /// </summary>
        public bool IsNullable
        {
            get
            {
                return isNullable;
            }
            set
            {
                if (isNullable != value)
                {
                    isNullable = value;
                    NotifyPropertyChanged(nameof(IsNullable));
                }
            }
        }

        private int fieldLength;
        /// <summary>
        /// 字段长度(字符类型/数字类型/文件类型适用)
        /// </summary>
        public int FieldLength
        {
            get
            {
                return fieldLength;
            }
            set
            {
                if (fieldLength != value)
                {
                    fieldLength = value;
                    NotifyPropertyChanged(nameof(FieldLength));
                }
            }
        }

        private short filedDecimals;
        /// <summary>
        /// 精度(小数类型适用)
        /// </summary>
        public short FieldDecimals
        {
            get
            {
                return filedDecimals;
            }
            set
            {
                if (filedDecimals != value)
                {
                    filedDecimals = value;
                    NotifyPropertyChanged(nameof(FieldDecimals));
                }
            }
        }

        private string unit;
        /// <summary>
        /// 单位(数字类型适用)
        /// </summary>
        public string Unit
        {
            get
            {
                return unit;
            }
            set
            {
                if (unit != value)
                {
                    unit = value;
                    NotifyPropertyChanged(nameof(Unit));
                }
            }
        }

        private bool isNegative;
        /// <summary>
        /// 是否可为负(数字类型适用)
        /// 非外键，且MIN_VALUE、MAX_VALUE为空时启用
        /// </summary>
        public bool IsNegative
        {
            get
            {
                return isNegative;
            }
            set
            {
                if (isNegative != value)
                {
                    isNegative = value;
                    NotifyPropertyChanged(nameof(IsNegative));
                }
            }
        }

        private double minValue;
        /// <summary>
        /// 最小值(数字类型适用)
        /// </summary>
        public double MinValue
        {
            get
            {
                return minValue;
            }
            set
            {
                if (minValue != value)
                {
                    minValue = value;
                    NotifyPropertyChanged(nameof(MinValue));
                }
            }
        }

        private double maxValue;
        /// <summary>
        /// 最大值(数字类型适用)
        /// </summary>
        public double MaxValue
        {
            get
            {
                return maxValue;
            }
            set
            {
                if (maxValue != value)
                {
                    maxValue = value;
                    NotifyPropertyChanged(nameof(MaxValue));
                }
            }
        }

        private string description;
        /// <summary>
        /// 描述
        /// </summary>
        public string Description
        {
            get
            {
                return description;
            }
            set
            {
                if (description != value)
                {
                    description = value;
                    NotifyPropertyChanged(nameof(Description));
                }
            }
        }
        private bool isCreateIndex;
        /// <summary>
        /// 是否创建索引
        /// </summary>
        public bool IsCreateIndex
        {
            get
            {
                return isCreateIndex;
            }
            set
            {
                if (isCreateIndex != value)
                {
                    isCreateIndex = value;
                    NotifyPropertyChanged(nameof(IsCreateIndex));
                }
            }
        }
    }
}
