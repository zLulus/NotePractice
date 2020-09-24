using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfDemo.PrintTable.Dtos
{
    public class OrderDetail
    {
        public string Sku { get; set; }
        public string Spec { get; set; }
        public decimal Number { get; set; }
        public string Unit { get; set; }
        public decimal UnitPrice { get; set; }
        public string Description { get; set; }
    }
}
