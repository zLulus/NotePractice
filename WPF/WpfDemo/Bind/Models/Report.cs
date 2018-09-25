using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfDemo.Bind.Models
{
    public class Report
    {
        /// <summary>
        /// 统计时间
        /// </summary>
        public string StatisticalDate { get; set; }
        public List<ReportDetail> ReportDetails { get; set; }
    }
}
