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
using WpfDemo.CustomDialogs;

namespace WpfDemo.ValidationForms.ValidationForm3
{
    /// <summary>
    /// Interaction logic for ValidationForm3Demo.xaml
    /// </summary>
    public partial class ValidationForm3Demo : UserControl
    {
        public ValidationForm3Demo()
        {
            InitializeComponent();
        }

        private void OpenValidationFormInDialog_Click(object sender, RoutedEventArgs e)
        {
            CustomDialog dialog = new CustomDialog(new ValidationFormInDialog(), "自定义弹窗中的表单验证");
            dialog.ShowDialog(() =>
            {
                return true;
            });
        }
    }
}
