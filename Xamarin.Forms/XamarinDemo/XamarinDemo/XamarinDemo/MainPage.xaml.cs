using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using XamarinDemo.DemoPages;

namespace XamarinDemo
{
	public partial class MainPage : ContentPage
	{
		public MainPage()
		{
			InitializeComponent();
            Button StackLayoutDemo1Button = new Button();
            StackLayoutDemo1Button.Clicked += ((sender,e)=>
            {
                Navigation.PushAsync(new StackLayoutExample());
            });
            StackLayoutDemo1Button.Text = "StackLayout+Label";
            Button StackLayoutDemo1Button2 = new Button();
            StackLayoutDemo1Button2.Clicked += ((sender,e)=> {
                Navigation.PushModalAsync(new ListViewInStackLayout());
            });
            StackLayoutDemo1Button2.Text = "StackLayout+ListView";

            //内容
            Content = new StackLayout
            {
                //间距
                Spacing = 10,
                Children =
                {
                    StackLayoutDemo1Button,
                    StackLayoutDemo1Button2
                }
            };
        }

        public void StackLayoutDemo1Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new StackLayoutExample());
        }

        public void StackLayoutDemo2Clicked(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new ListViewInStackLayout());
        }
    }
}
