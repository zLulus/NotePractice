using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace WpfDemo.DynamicallyGeneratedDataGrid.Bases
{
    public class SetDataColumnsItem
    {
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
        /// <summary>
        /// 列宽方式
        /// </summary>
        public DataGridLengthUnitType DataGridLengthUnitType { get; set; }
    }
}
