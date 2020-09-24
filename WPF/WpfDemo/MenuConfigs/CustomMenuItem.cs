using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfDemo.MenuConfigs
{
    public class CustomMenuItem
    {
        public string Title { get; set; }
        public string DllName { get; set; }
        public string ClassName { get; set; }
        public List<CustomMenuItem> Children { get; set; }
    }
}
