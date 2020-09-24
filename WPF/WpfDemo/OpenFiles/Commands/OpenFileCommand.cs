using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace WpfDemo.OpenFiles.Commands
{
    public class OpenFileCommand : ICommand
    {
        public event Action<object> OnOpened;
        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            OnOpened?.Invoke(parameter);
        }
    }
}
