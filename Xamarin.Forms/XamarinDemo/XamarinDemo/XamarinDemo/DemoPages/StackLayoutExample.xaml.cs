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
            //同时设置xaml和cs代码,会以cs代码为准
            var red = new Label
            {
                Text = "Stop",
                BackgroundColor = Color.AliceBlue,
                FontSize = 20
            };
            var yellow = new Label
            {
                Text = "Slow down",
                BackgroundColor = Color.Pink,
                FontSize = 20
            };
            var green = new Label
            {
                Text = "Go",
                BackgroundColor = Color.YellowGreen,
                FontSize = 20
            };

            //内容
            Content = new StackLayout
            {
                //间距
                Spacing = 10,
                //垂直方向上，从底部出发
                VerticalOptions = LayoutOptions.End,
                //堆放三个Label的方向是水平
                Orientation = StackOrientation.Horizontal,
                //水平方向上，从开始（左边）出发
                HorizontalOptions = LayoutOptions.Start,
                Children = { red, yellow, green }
            };
        }
    }
}
