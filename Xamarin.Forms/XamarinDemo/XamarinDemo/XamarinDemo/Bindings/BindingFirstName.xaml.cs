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
    public sealed partial class BindingFirstName : ContentPage
    {
        public BindingFirstName()
        {
            //Model
            Employee employeeToDisplay = new Employee();

            //输入框
            var firstNameEntry = new Entry()
            {
                HorizontalOptions = LayoutOptions.FillAndExpand
            };
            //绑定  下面两个绑定，任选其一即可
            this.BindingContext = employeeToDisplay;
            firstNameEntry.SetBinding(Entry.TextProperty, "FirstName");

            //查询按钮
            Button getValueButton = new Button();
            getValueButton.Text = "查看结果";
            getValueButton.Clicked += (async (sender, e) =>
            {
                await DisplayAlert("绑定结果",$"当前Entry的Text是{firstNameEntry.Text},后台实体的FirstName是{employeeToDisplay.FirstName}","确定");
            });

            Content = new StackLayout
            {
                VerticalOptions = LayoutOptions.FillAndExpand,
                Children =
                {
                    firstNameEntry,
                    getValueButton
                }
            };
        }
    }
}
