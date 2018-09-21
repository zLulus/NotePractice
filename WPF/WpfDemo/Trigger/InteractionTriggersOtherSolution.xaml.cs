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
using WpfDemo.Trigger.Models;

namespace WpfDemo.Trigger
{
    /// <summary>
    /// Interaction logic for InteractionTriggersNextSolution.xaml
    /// </summary>
    public partial class InteractionTriggersOtherSolution : Window
    {
        public InteractionTriggersOtherSolution()
        {
            InitializeComponent();
        }

        public void ShowDialog(object sender, ShowDialogEventArgs e)
        {
            MessageBox.Show("ShowDialog");
        }

        public void AppointmentEditing(object sender, AppointmentEditingEventArgs e)
        {
            MessageBox.Show("AppointmentEditing");
        }
    }
}
