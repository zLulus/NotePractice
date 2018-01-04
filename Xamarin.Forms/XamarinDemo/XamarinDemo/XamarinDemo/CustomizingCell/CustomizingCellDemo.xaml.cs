using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XamarinDemo.CustomizingCell.Cells;
using XamarinDemo.CustomizingCell.Models;

namespace XamarinDemo.CustomizingCell
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class CustomizingCellDemo : ContentPage
	{
		public CustomizingCellDemo ()
		{
			InitializeComponent ();
            List<Employee> EmployeeList = new List<Employee>();
            EmployeeList.Add(new Employee()
            {
                DisplayName = "Jack",
                Twitter = "@fake4",
                ImageUri = "http://v1.qzone.cc/avatar/201406/24/21/03/53a977066f053731.jpg!200x200.jpg"
            });
            EmployeeList.Add(new Employee()
            {
                DisplayName = "Tom",
                Twitter = "@mml1",
                ImageUri = "http://diy.qqjay.com/u2/2014/0628/da687c0fb5b3bb8cd31dec7d8865aea8.jpg"
            });
            EmployeeList.Add(new Employee()
            {
                DisplayName = "Tony",
                Twitter = "@wood564",
                ImageUri = "http://v1.qzone.cc/avatar/201406/24/21/03/53a977066f053731.jpg!200x200.jpg"
            });
            var listView = new ListView
            {
                RowHeight = 80
            };
            listView.ItemsSource = EmployeeList;
            //注意：此时指定模板为写好的EmployeeCell
            listView.ItemTemplate = new DataTemplate(typeof(EmployeeCell));
            Content = new StackLayout
            {
                VerticalOptions = LayoutOptions.FillAndExpand,
                Children = { listView }
            };
        }
	}
}