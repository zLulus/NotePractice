using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using WpfDemo.ValidationForms.ValidationForm3;

namespace WpfDemo.CustomDialogs
{
    /// <summary>
    /// Interaction logic for CustomDialog.xaml
    /// </summary>
    public partial class CustomDialog : Window
    {
        #region GetTopWindow
        //从Handle中获取Window对象
        static Window GetWindowFromHwnd(IntPtr hwnd)
        {
            var window = HwndSource.FromHwnd((IntPtr)hwnd);
            dynamic customWindow = window.RootVisual;
            return customWindow;
        }

        //GetForegroundWindow API
        [DllImport("user32.dll")]
        static extern IntPtr GetForegroundWindow();

        //调用GetForegroundWindow然后调用GetWindowFromHwnd
        static Window GetTopWindow()
        {
            var hwnd = GetForegroundWindow();
            if (hwnd == null)
                return null;

            return GetWindowFromHwnd(hwnd);
        }
        #endregion

        public CustomDialog(UserControl control, string title, Window window = null)
        {
            InitializeComponent();

            Title = title;

            //ShowInTaskbar="False"
            //设置Owner防止假死
            if (window != null)
                Owner = window;
            else
            {
                var o = GetTopWindow();
                if (o != null)
                    Owner = o;
                else if (Window.GetWindow(control) != null)
                    Owner = Window.GetWindow(control);
            }

            //设置内容
            contentContainer.Content = control;

            //绑定提交按钮是否可用
            Type t = control.DataContext.GetType();//获得该类的Type
            var property = t.GetProperties().Where(x => x.Name == nameof(ViewModelWithValidation.IsSubmitButtonEnable)).FirstOrDefault();
            if (property != null)
            {
                btnOK.DataContext = control.DataContext;
                btnOK.SetBinding(Button.IsEnabledProperty, new Binding(nameof(ViewModelWithValidation.IsSubmitButtonEnable))
                {
                    Mode = BindingMode.OneWay,
                    UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged
                });
            }
        }

        public void ShowDialog(Func<bool> onOKCallback)
        {
            btnOK.Click += (s1, e1) => {
                try
                {
                    var fOK = true;
                    if (onOKCallback != null)
                    {
                        fOK = onOKCallback();
                    }
                    if (fOK)
                    {
                        Close();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "错误", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            };
            ShowDialog();
        }
    }
}
