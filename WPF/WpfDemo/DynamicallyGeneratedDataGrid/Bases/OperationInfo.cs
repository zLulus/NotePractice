using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace WpfDemo.DynamicallyGeneratedDataGrid.Bases
{
    public class OperationInfo
    {
        public string Content { get; set; }
        public RoutedEventHandler ExecuteEvent { get; set; }
        public IValueConverter CanExecuteEvent { get; set; }
        /// <summary>
        /// 排序
        /// </summary>
        public short Order { get; set; }
    }
}
