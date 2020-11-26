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
        private bool isCanChecked;
        /// <summary>
        /// 是否可以被选择
        /// </summary>
        public bool IsCanChecked
        {
            set
            {
                isCanChecked = value;
                OnPropertyChanged(nameof(IsCanChecked));
            }
            get
            {
                return isCanChecked;

            }
        }
        private bool isChecked;
        /// <summary>
        /// 是否被选择
        /// </summary>
        public bool IsChecked
        {
            set
            {
                isChecked = value;
                OnPropertyChanged(nameof(IsChecked));
            }
            get
            {
                return isChecked;

            }
        }
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
            IsCanChecked = false;
            IsChecked = false;
            Children = new ObservableCollection<AdministrationViewModel>();
        }
    }
}
