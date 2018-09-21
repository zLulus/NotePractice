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

namespace WpfDemo.Trigger
{
    /// <summary>
    /// Interaction logic for InteractionTriggers.xaml
    /// </summary>
    public partial class InteractionTriggers : Window
    {
        public InteractionTriggers()
        {
            InitializeComponent();
            DataContext = this;
        }

        //#region CommandWithEventArgs
        //DelegateCommand<MouseEventArgs> _CommandWithEventArgs;
        ///// <summary>
        ///// Exposes <see cref="CommandWithEventArgs(MouseEventArgs)"/>.
        ///// </summary>
        //public DelegateCommand<MouseEventArgs> CommandWithEventArgs
        //{
        //    get { return _CommandWithEventArgs ?? (_CommandWithEventArgs = new DelegateCommand<MouseEventArgs>(CommandWithEventArgs)); }
        //}
        //#endregion
        //public void CommandWithEventArgs(MouseEventArgs param)
        //{
        //}
    }
}
