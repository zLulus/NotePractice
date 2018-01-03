using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.AngularjsStudy
{
    public class District
    {
        public int id { get; set; }
        /// <summary>
        /// 根节点FatherID=0
        /// </summary>
        public int fatherID { get; set; }
        public string name { get; set; }
    }
}
