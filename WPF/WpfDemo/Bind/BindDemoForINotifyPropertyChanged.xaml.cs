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
using WpfDemo.Bind.Models;

namespace WpfDemo.Bind
{
    /// <summary>
    /// BindDemo.xaml 的交互逻辑
    /// </summary>
    public partial class BindDemoForINotifyPropertyChanged : UserControl
    {
        public BindDemoForINotifyPropertyChanged()
        {
            InitializeComponent();
            Student student = new Student()
            {
                Name = "Lulu"
            };
            DataContext = student;
        }
    }
}
