using System;
using System.Threading.Tasks;
using Windows.UI.Core;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;

//https://go.microsoft.com/fwlink/?LinkId=234236 上介绍了“用户控件”项模板

namespace UWPDemo.UserControls
{
    public sealed partial class DispatcherDemo : UserControl
    {
        public DispatcherDemo()
        {
            this.InitializeComponent();
        }

        private void TestDispatcher_Tapped(object sender, TappedRoutedEventArgs e)
        {
            //https://stackoverflow.com/questions/41016213/getting-the-dispatcher-of-a-window-in-uwp
            Task.Run(async () =>
            {
                await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, async () =>
                 {
                     ContentDialog dialog = new ContentDialog()
                     {
                         Title = "Test Dispatcher(测试Dispatcher)",
                         DefaultButton = ContentDialogButton.Close,
                         CloseButtonText = "Close(关闭)"
                     };
                     await dialog.ShowAsync();
                 });
            });
        }
    }
}
