using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Models.AboutDB
{
    public class Area
    {
        public int PKID { get; set; }
        public int ParentID { get; set; }
        public string Name { get; set; }
    }
}
