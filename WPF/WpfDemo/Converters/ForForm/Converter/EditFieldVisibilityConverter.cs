using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace WpfDemo.Converters.ForForm
{
    /// <summary>
    /// 新增字段-选择字段类型，控制组件显隐
    /// </summary>
    public class EditFieldVisibilityConverter : IValueConverter
    {
        //当值从绑定源传播给绑定目标时，调用方法Convert
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var enumInfo = value as EnumInfo;
            //初始化加载
            if (enumInfo == null)
                return Visibility.Collapsed;
            var fieldName = parameter.ToString();
            var fieldType = (FieldTypeEnum)enumInfo.Value;
            //只判断不显示的状态
            var isNumber = fieldType==FieldTypeEnum.Double || fieldType==FieldTypeEnum.Int
                || fieldType==FieldTypeEnum.Long || fieldType==FieldTypeEnum.Decimal;
            switch (fieldName)
            {
                case nameof(EditFieldMetaViewModel.IsUniqueKey):
                    if (!isNumber && fieldType != FieldTypeEnum.String)
                    {
                        return Visibility.Collapsed;
                    }
                    break;
                case nameof(EditFieldMetaViewModel.IsNullable):
                    break;
                case nameof(EditFieldMetaViewModel.FieldLength):
                    if (!isNumber && fieldType != FieldTypeEnum.String)
                    {
                        return Visibility.Collapsed;
                    }
                    break;
                case nameof(EditFieldMetaViewModel.FieldDecimals):
                    if (fieldType!=FieldTypeEnum.Decimal && fieldType!=FieldTypeEnum.Double)
                    {
                        return Visibility.Collapsed;
                    }
                    break;
                case nameof(EditFieldMetaViewModel.Unit):
                    if (!isNumber)
                    {
                        return Visibility.Collapsed;
                    }
                    break;
                case nameof(EditFieldMetaViewModel.IsNegative):
                    if (!isNumber)
                    {
                        return Visibility.Collapsed;
                    }
                    break;
                case nameof(EditFieldMetaViewModel.MinValue):
                    if (!isNumber)
                    {
                        return Visibility.Collapsed;
                    }
                    break;
                case nameof(EditFieldMetaViewModel.MaxValue):
                    if (!isNumber)
                    {
                        return Visibility.Collapsed;
                    }
                    break;
                case nameof(EditFieldMetaViewModel.IsCreateIndex):
                    break;
            }
            return Visibility.Visible;
        }
        //当值从绑定目标传播给绑定源时，调用此方法ConvertBack
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new Exception("");
        }
    }
}
