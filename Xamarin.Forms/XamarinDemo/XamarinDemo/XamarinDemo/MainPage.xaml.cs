using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using XamarinDemo.Bindings;
using XamarinDemo.CustomizingCell;
using XamarinDemo.DemoPages;

namespace XamarinDemo
{
	public partial class MainPage : ContentPage
	{
        public MainPage()
        {
            InitializeComponent();
            
        }

        private void SliderBindingsPageClicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new SliderBindingsPage());
        }

        private void BindingFirstName2Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new BindingFirstName2());
        }

        private void BindingFirstNameClicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new BindingFirstName());
        }

        private void ShowTodoItemButton1Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new ShowTodoItem());
        }

        private void CustomizingCellDemoButton1Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new CustomizingCellDemo());
        }

        private void CustomizingCellDemoButton2Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new CustomizingCellDemo2());
        }

        public void StackLayoutDemo1Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new StackLayoutExample());
        }

        public void StackLayoutDemo2Clicked(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new ListViewInStackLayout());
        }

        public void BindingHeightClicked(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new BindingHeight());
        }
    }
}
