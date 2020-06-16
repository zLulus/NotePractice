using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfDemo.SingleSelectors.ViewModels
{
    public class SingleSelectorViewModel
    {
        public long Id { get; set; }
        public long ParendId { get; set; }
        public string Name { get; set; }
        public List<SingleSelectorViewModel> Children { get; set; }
    }
}
