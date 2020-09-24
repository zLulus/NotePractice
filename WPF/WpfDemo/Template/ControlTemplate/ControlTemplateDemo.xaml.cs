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

namespace WpfDemo.Template.ControlTemplate
{
    /// <summary>
    /// ControlTemplateDemo.xaml 的交互逻辑
    /// </summary>
    public partial class ControlTemplateDemo : UserControl
    {
        public ControlTemplateDemo()
        {
            InitializeComponent();
        }

        private void ModifyTextBlockInTemplate_Click(object sender, RoutedEventArgs e)
        {
            var template = checkBox1.Template;
            var myControl = template.FindName("textBlock1", checkBox1);
            var tb = myControl as TextBlock;
            tb.Background = new SolidColorBrush(Colors.LightPink);
        }
    }
}
