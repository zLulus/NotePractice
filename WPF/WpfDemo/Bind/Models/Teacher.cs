using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfDemo.Bind.Models
{
    public class Teacher
    {
        public string SchoolNumber { get; set; }
        public string Name { get; set; }
        public string Sex { get; set; }
        public TeacherDetailInfo TeacherDetailInfo { get; set; }
    }
}
