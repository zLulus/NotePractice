using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfDemo.TreeViewBind.ViewModels
{
    public class DisplayViewModel
    {
        public string Name { get; set; }
        public List<DisplayViewModel> Children { get; set; }
    }
}
