using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace HandwritingDemo.ViewModel
{
    public class AlternateViewModel : NotifyPropertyChangedBase
    {
        private HandwritingRecognizeViewModel _handwritingRecognizeViewModel;
        private string _name;
        public string Name
        {
            get { return _name; }

            private set
            {
                if (_name != value)
                {
                    _name = value;
                    RaisePropertyChanged(nameof(Name));
                }
            }
        }

        public ICommand TextSelectCommand { get; }

        public AlternateViewModel(HandwritingRecognizeViewModel handwritingRecognizeViewModel,string name)
        {
            _handwritingRecognizeViewModel = handwritingRecognizeViewModel;
            Name = name;
            TextSelectCommand = new Command<AlternateViewModel>(TextSelect);
        }

        private void TextSelect(AlternateViewModel text)
        {
            _handwritingRecognizeViewModel.InputText += text.Name;
            _handwritingRecognizeViewModel.Clear();
        }

    }
}
