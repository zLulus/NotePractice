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
using WpfDemo.ValidationForms.ViewModels;

namespace WpfDemo.ValidationForms.ValidationForm1
{
    /// <summary>
    /// Interaction logic for ValidationFormInOneUserControl.xaml
    /// </summary>
    public partial class ValidationFormInOneUserControl : UserControl
    {
        ValidationFormViewModel vm { get; set; }
        public ValidationFormInOneUserControl()
        {
            InitializeComponent();

            vm = new ValidationFormViewModel();
            DataContext = vm;
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            var b1 = Validation.GetHasError(nameTextBox);
            var b2 = Validation.GetHasError(ageTextBox);
            var b3 = Validation.GetHasError(remarkTextBox);

            submitButton.IsEnabled = !b1 && !b2 && !b3;
        }
    }
}
