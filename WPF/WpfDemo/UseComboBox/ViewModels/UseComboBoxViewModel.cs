using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfDemo.UseComboBox.ViewModels
{
    public class UseComboBoxViewModel
    {
        public ObservableCollection<FruitViewModel> Fruits { get; set; }
        public FruitViewModel SelectFruit { get; set; }
        public FruitViewModel SelectValueFruit { get; set; }
        public string SelectValueFruitName { get; set; }
    }
}
