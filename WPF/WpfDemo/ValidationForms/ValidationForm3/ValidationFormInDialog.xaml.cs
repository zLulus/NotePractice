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

namespace WpfDemo.ValidationForms.ValidationForm3
{
    /// <summary>
    /// Interaction logic for ValidationFormInDialog.xaml
    /// </summary>
    public partial class ValidationFormInDialog : UserControl
    {
        ValidationFormInDialogViewModel vm { get; set; }
        public ValidationFormInDialog()
        {
            InitializeComponent();

            vm = new ValidationFormInDialogViewModel();
            DataContext = vm;
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            vm.ValidationInputs(new List<DependencyObject>()
            {
                nameTextBox,
                ageTextBox,
                remarkTextBox
            });
        }
    }
}
