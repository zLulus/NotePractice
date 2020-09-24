using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfDemo.ItemsControls.ViewModels
{
    public class EditDataModelOneTableViewModel
    {
        public ObservableCollection<EditDataModelItemViewModel> DataModelItems { get; set; }
        public EditDataModelEditionViewModel DataModelEdition { get; set; }
    }
}
