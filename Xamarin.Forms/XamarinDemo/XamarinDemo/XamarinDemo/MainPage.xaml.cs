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

            Label NameLabel = new Label();
            NameLabel.Text = "Demo列表";

            //Button StackLayoutDemo1Button = new Button();
            //StackLayoutDemo1Button.Clicked += StackLayoutDemo1Clicked;
            //StackLayoutDemo1Button.Text = "StackLayout+Label";

            //Button StackLayoutDemo1Button2 = new Button();
            //StackLayoutDemo1Button2.Clicked += StackLayoutDemo2Clicked;
            //StackLayoutDemo1Button2.Text = "StackLayout+ListView";

            //Button CustomizingCellDemoButton1 = new Button();
            //CustomizingCellDemoButton1.Clicked += CustomizingCellDemoButton1Clicked;
            //CustomizingCellDemoButton1.Text = "自定义单元格Demo";

            ////Button ShowTodoItemButton1 = new Button();
            ////ShowTodoItemButton1.Clicked += ShowTodoItemButton1Clicked;
            ////ShowTodoItemButton1.Text = "数据绑定";

            //Button BindingFirstNameButton = new Button();
            //BindingFirstNameButton.Clicked += BindingFirstNameClicked;
            //BindingFirstNameButton.Text = "数据绑定(Binding FirstName - C#)";

            Button BindingFirstName2Button = new Button();
            BindingFirstName2Button.Clicked += BindingFirstName2Clicked;
            BindingFirstName2Button.Text = "数据绑定(Binding FirstName - XAML)";

            Button SliderBindingsPageButton = new Button();
            SliderBindingsPageButton.Clicked += SliderBindingsPageClicked;
            SliderBindingsPageButton.Text = "数据绑定(Slider - XAML)";

            //内容
            Content = new StackLayout
            {
                //间距
                Spacing = 10,
                Children =
                {
                    NameLabel,
                    //StackLayoutDemo1Button,
                    //StackLayoutDemo1Button2,
                    //CustomizingCellDemoButton1,
                    ////ShowTodoItemButton1,
                    //BindingFirstNameButton,
                    BindingFirstName2Button,
                    SliderBindingsPageButton,
                }
            };
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
