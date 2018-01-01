using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Xamarin.Forms;

// https://go.microsoft.com/fwlink/?LinkId=234238 上介绍了“空白页”项模板

namespace XamarinDemo.DemoPages
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public partial class StackLayoutExample : ContentPage
    {
        public StackLayoutExample()
        {
            var red = new Label
            {
                Text = "Stop",
                BackgroundColor = Color.Red,
                FontSize = 20
            };
            var yellow = new Label
            {
                Text = "Slow down",
                BackgroundColor = Color.Yellow,
                FontSize = 20
            };
            var green = new Label
            {
                Text = "Go",
                BackgroundColor = Color.Green,
                FontSize = 20
            };

            Content = new StackLayout
            {
                Spacing = 10,
                Children = { red, yellow, green }
            };
        }
    }
}
