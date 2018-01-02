using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Xamarin.Forms;
using XamarinDemo.CustomizingCell.Models;

// https://go.microsoft.com/fwlink/?LinkId=234238 上介绍了“空白页”项模板

namespace XamarinDemo.CustomizingCell
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class CustomizingCellDemo2 : ContentPage
    {
        public static List<Employee> Employees;
        public CustomizingCellDemo2()
        {
            //todo 待完成
            Employees = new List<Employee>();
            Employees.Add(new Employee()
            {
                DisplayName = "Jack",
                Twitter = "@fake4",
                ImageUri = "http://v1.qzone.cc/avatar/201406/24/21/03/53a977066f053731.jpg!200x200.jpg"
            });
            Employees.Add(new Employee()
            {
                DisplayName = "Tom",
                Twitter = "@mml1",
                ImageUri = "http://diy.qqjay.com/u2/2014/0628/da687c0fb5b3bb8cd31dec7d8865aea8.jpg"
            });
            Employees.Add(new Employee()
            {
                DisplayName = "Tony",
                Twitter = "@wood564",
                ImageUri = "http://v1.qzone.cc/avatar/201406/24/21/03/53a977066f053731.jpg!200x200.jpg"
            });
        }
    }
}
