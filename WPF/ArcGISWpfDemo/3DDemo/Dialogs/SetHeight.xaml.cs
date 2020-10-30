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

namespace ArcGIS3D.WpfDemo.Dialogs
{
    /// <summary>
    /// Interaction logic for SetHeight.xaml
    /// </summary>
    public partial class SetHeight : Window
    {
        public double height = 0;
        public SetHeight()
        {
            InitializeComponent();
        }

        private void SetHeight_Click(object sender, RoutedEventArgs e)
        {
            double.TryParse(heightTextBox.Text, out height);
            if (height <= 0)
            {
                MessageBox.Show("错误的高度");
                return;
            }
            this.DialogResult = true;
        }
    }
}
