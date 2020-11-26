using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfDemo.PopupWithTreeView.ViewModels
{
    public class AdministrationViewModel : INotifyPropertyChanged
    {
        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

        private string id;
        public string Id
        {
            set
            {
                id = value;
                OnPropertyChanged(nameof(Id));
            }
            get
            {
                return id;

            }
        }
        private short level;
        public short Level
        {
            set
            {
                level = value;
                OnPropertyChanged(nameof(Level));
            }
            get
            {
                return level;

            }
        }
        private string code;
        public string Code
        {
            set
            {
                code = value;
                OnPropertyChanged(nameof(Code));
            }
            get
            {
                return code;

            }
        }
        //private bool isChecked;
        //public bool IsChecked
        //{
        //    set
        //    {
        //        isChecked = value;
        //        OnPropertyChanged(nameof(IsChecked));
        //    }
        //    get
        //    {
        //        return isChecked;

        //    }
        //}
        private string name;
        public string Name
        {
            set
            {
                name = value;
                OnPropertyChanged(nameof(Name));
            }
            get
            {
                return name;

            }
        }
        public ObservableCollection<AdministrationViewModel> Children { get; set; }
        public AdministrationViewModel()
        {
            Children = new ObservableCollection<AdministrationViewModel>();
        }
    }
}
