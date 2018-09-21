using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using WpfDemo.Command.Commands;
using WpfDemo.Command.ViewModels;

namespace WpfDemo.Command
{
    /// <summary>
    /// Interaction logic for CommandDemoWithBindParameters.xaml
    /// </summary>
    public partial class CommandDemoWithBindParameters : Window
    {
        CommandDemoWithBindParametersViewModel vm;
        public CommandDemoWithBindParameters()
        {
            InitializeComponent();
            vm = new CommandDemoWithBindParametersViewModel();
            vm.Person=new Person()
            {
                Name="请输入姓名",
                Age="请输入年龄"
            }
            DataContext = vm;
        }
    }
}
