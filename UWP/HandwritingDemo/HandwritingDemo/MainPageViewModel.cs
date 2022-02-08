using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HandwritingDemo
{
    public class MainPageViewModel: NotifyPropertyChangedBase
    {
        private ObservableCollection<string> _alternates;

        /// <summary>
        /// Get 候选词列表
        /// </summary>
        public ObservableCollection<string> Alternates
        {
            get { return _alternates; }

            private set
            {
                if (_alternates != value)
                {
                    _alternates = value;
                    RaisePropertyChanged(nameof(Alternates));
                }
            }
        }

        public MainPageViewModel()
        {
            Alternates = new ObservableCollection<string>();
        }
    }
}
