using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Xamarin.Forms;
using XamarinDemo.Bindings.Models;

// https://go.microsoft.com/fwlink/?LinkId=234238 上介绍了“空白页”项模板

namespace XamarinDemo.Bindings
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class ShowTodoItem : ContentPage
    {
        public ShowTodoItem()
        {
            var listView = new Xamarin.Forms.ListView
            {
                RowHeight = 40
            };
            listView.ItemsSource = new TodoItem[] {
                new TodoItem { Name = "Buy 2 pears" },
                new TodoItem { Name = "Buy 3 oranges", Done=true} ,
                new TodoItem { Name = "Buy 5 mangos" },
                new TodoItem { Name = "Buy 7 apples", Done=true },
                new TodoItem { Name = "Buy 8 bananas", Done=true }
            };
            //TextCell是内置默认模板
            listView.ItemTemplate = new DataTemplate(typeof(TextCell));
            //绑定TextCell模板的Text属性对应TodoItem的Name
            listView.ItemTemplate.SetBinding(TextCell.TextProperty, "Name");
            //添加Item选中事件
            listView.ItemSelected += async (sender, e) => {
                await DisplayAlert("Tapped!", e.SelectedItem + " was tapped.", "OK");
            };
            Content = new StackLayout
            {
                VerticalOptions = LayoutOptions.FillAndExpand,
                Children = { listView }
            };
        }
    }
}
