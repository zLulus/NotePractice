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
    public sealed partial class BindingFirstName2 : ContentPage
    {
        public BindingFirstName2()
        {
            Employee employee = new Employee();
            employee.FirstName = "Test FirstName";
            this.BindingContext = employee;
        }
    }
}
