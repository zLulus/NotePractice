using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfDemo.PropertyChanged.Models;

namespace WpfDemo.PropertyChanged.ViewModels
{
    public class ChangeStudentInfoViewModel
    {
        public StudentByINotifyPropertyChanged TextBoxViewModel { get; set; }
        public StudentByINotifyPropertyChanged InfoViewModel { get; set; }
    }
}
