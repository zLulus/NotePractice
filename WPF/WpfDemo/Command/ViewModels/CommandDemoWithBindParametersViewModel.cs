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
    public class CommandDemoWithBindParametersViewModel
    {
        public Person Person { get; set; }
        public ICommand SubmitCommand => _submitCommand;
        private DelegateCommand _submitCommand = new DelegateCommand(new Action<object>(ShowMessage));
        private static void ShowMessage(object obj)
        {
            Person person = obj as Person;
            MessageBox.Show($"{person.Name},{person.Age}");
        }
    }
}
