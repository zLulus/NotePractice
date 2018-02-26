using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace XamarinDemo.DemoPages
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ListViewInStackLayout : ContentPage
	{
		public ListViewInStackLayout ()
		{
			InitializeComponent ();
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