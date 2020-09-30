using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfDemo.ValidationForms.ValidationForm3
{
    public class ValidationFormInDialogViewModel : ViewModelWithValidation
    {
        private String name;
        public String Name
        {
            get { return name; }
            set
            {
                if (name != value)
                {
                    name = value;
                    NotifyPropertyChanged(nameof(Name));
                }
            }
        }
        private String age;
        public String Age
        {
            get { return age; }
            set
            {
                if (age != value)
                {
                    age = value;
                    NotifyPropertyChanged(nameof(Age));
                }
            }
        }
        private String remark;
        public String Remark
        {
            get { return remark; }
            set
            {
                if (remark != value)
                {
                    remark = value;
                    NotifyPropertyChanged(nameof(Remark));
                }
            }
        }
    }
}
