using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace WcfServicePractice.Model
{
    [DataContract]
    public class Student
    {
        [DataMember]
        public int PKID { get; set; }
        [DataMember]
        public string StudentNumber { get; set; }
        [DataMember]
        public string Name { get; set; }

    }
}
