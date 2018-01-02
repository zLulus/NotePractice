using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XamarinDemo.CustomizingCell.Models;

namespace XamarinDemo.CustomizingCell
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class CustomizingCellDemo2 : ContentPage
	{
		public CustomizingCellDemo2 ()
		{
			InitializeComponent ();
            List<Employee> Employees = new List<Employee>();

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

            BindingContext = Employees;
        }
	}
}