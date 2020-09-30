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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfDemo.ValidationForms.ValidationForm2
{
    /// <summary>
    /// Interaction logic for ValidationFormInOneUserControl2.xaml
    /// </summary>
    public partial class ValidationFormInOneUserControl2 : UserControl
    {
        public ValidationFormInOneUserControl2()
        {
            InitializeComponent();

            Person p = new Person();
            MainGrid.DataContext = p;
        }
    }
}
