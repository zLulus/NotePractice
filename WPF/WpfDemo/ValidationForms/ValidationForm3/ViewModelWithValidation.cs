using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace WpfDemo.ValidationForms.ValidationForm3
{
    public class ViewModelWithValidation :  INotifyPropertyChanged
    {
        #region INotifyPropertyChanged Members

        /// <summary>
        /// Need to implement this interface in order to get data binding
        /// to work properly.
        /// </summary>
        /// <param name="propertyName"></param>
        public void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion


        private bool isSubmitButtonEnable;

        public bool IsSubmitButtonEnable
        {
            get { return isSubmitButtonEnable; }
            set
            {
                if (isSubmitButtonEnable != value)
                {
                    isSubmitButtonEnable = value;
                    NotifyPropertyChanged(nameof(IsSubmitButtonEnable));
                }
            }
        }

        public void ValidationInputs(List<DependencyObject> dependencyObjects)
        {
            var isEnable = true;
            foreach (var dependencyObject in dependencyObjects)
            {
                isEnable = isEnable && !Validation.GetHasError(dependencyObject);
                if (isEnable == false)
                    break;
            }
            IsSubmitButtonEnable = isEnable;
        }
    }
}
