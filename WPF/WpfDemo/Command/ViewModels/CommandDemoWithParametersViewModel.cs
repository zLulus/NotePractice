using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using WpfDemo.Command.Commands;

namespace WpfDemo.Command.ViewModels
{
    public class CommandDemoWithParametersViewModel
    {
        public ICommand SubmitCommand => _submitCommand;
        private RelayCommand _submitCommand = new RelayCommand(new Action<object>(ShowMessage));
        private static void ShowMessage(object obj)
        {
            MessageBox.Show(obj.ToString());
        }
    }
}
