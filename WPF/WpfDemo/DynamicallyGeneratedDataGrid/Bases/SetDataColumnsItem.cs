using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Data;

namespace WpfDemo.DynamicallyGeneratedDataGrid.Bases
{
    public class SetDataColumnsItem
    {
        public SetDataColumnsItem()
        {
            ColumnType = ColumnTypeEnum.Label;
        }
        /// <summary>
        /// 列名
        /// </summary>
        public string Header { get; set; }
        /// <summary>
        /// 绑定字段
        /// </summary>
        public string BindPath { get; set; }
        /// <summary>
        /// 排序
        /// </summary>
        public short Order { get; set; }
        /// <summary>
        /// 列宽占比
        /// </summary>
        public double DataGridLengthValue { get; set; }
        public ColumnTypeEnum ColumnType { get; set; }
        /// <summary>
        /// 列宽方式
        /// </summary>
        public DataGridLengthUnitType DataGridLengthUnitType { get; set; }
        /// <summary>
        /// 值显示转换
        /// </summary>
        public IValueConverter DisplayEvent { get; set; }
        #region ComboBox
        /// <summary>
        /// ComboBox显示字段
        /// </summary>
        public string ComboBoxDisplayMemberPath { get; set; }
        /// <summary>
        /// 数据源
        /// </summary>
        public object ComboBoxDataContext { get; set; }
        /// <summary>
        /// 选中项绑定路径
        /// </summary>
        public string ComboBoxSelectedItemBindPath { get; set; }
        #endregion
    }
}
