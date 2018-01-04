using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XamarinDemo.Bindings.Models;

namespace XamarinDemo.Bindings
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ShowTodoItem : ContentPage
	{
		public ShowTodoItem ()
		{
			InitializeComponent ();
            var listView = new Xamarin.Forms.ListView
            {
                RowHeight = 40
            };
            listView.ItemsSource = new TodoItem[] {
                new TodoItem { Count=2,Item="pears" },
                new TodoItem { Count=3,Item="oranges", Done=true} ,
                new TodoItem { Count=5,Item="mangos" },
                new TodoItem { Count=7,Item="apples", Done=true },
                new TodoItem { Count=8,Item="bananas", Done=true }
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