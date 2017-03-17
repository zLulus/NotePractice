using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ContosoUniversity.Models
{
    public class Student
    {
        public int ID { get; set; }
        public string LastName { get; set; }
        public string FirstMidName { get; set; }
        //DateTime数据类型使EnrollmentDate属性在输入的时候得到验证
        public DateTime EnrollmentDate { get; set; }

        public string Secret { get; set; }

        public virtual ICollection<Enrollment> Enrollments { get; set; }
    }
}