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
    public sealed partial class ListViewInStackLayout : ContentPage
    {
        public ListViewInStackLayout()
        {
            var listView = new Xamarin.Forms.ListView
            {
                RowHeight = 40
            };
            listView.ItemsSource = new string[]
            {
                "Buy pears",
                "Buy oranges",
                "Buy mangos",
                "Buy apples",
                "Buy bananas"
            };
            var backButton = new Button();
            backButton.Text = "返回";
            backButton.Clicked += ((sender, e) =>
            {
                Navigation.PopModalAsync();
            });
            Content = new StackLayout
            {
                VerticalOptions = LayoutOptions.FillAndExpand,
                Children = { listView, backButton }
            };
        }
    }
}
