using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfDemo.HistoricalDataManage.Models
{
    public class StudentScore
    {
        //public long Id { get; set; }
        public string Name { get; set; }

        public int Score { get; set; }
        public DateTime CreateTime { get; set; }
    }
}
