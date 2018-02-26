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
	public partial class StackLayoutExample : ContentPage
	{
		public StackLayoutExample()
		{
            //同时设置xaml和cs代码,哪个在后面，以哪个为准，相当于被覆盖了
            //(详见region xaml和region C#)

            #region xaml
            //这里画界面上的内容
            InitializeComponent();
            #endregion

            #region C#
            var red = new Label
            {
                Text = "Stop",
                BackgroundColor = Color.AliceBlue,
                FontSize = 20
            };
            var yellow = new Label
            {
                Text = "Slow down",
                BackgroundColor = Color.Pink,
                FontSize = 20
            };
            var green = new Label
            {
                Text = "Go",
                BackgroundColor = Color.YellowGreen,
                FontSize = 20
            };
            var backButton = new Button();
            backButton.Text = "返回";
            backButton.Clicked += ((sender, e) =>
            {
                Navigation.PopAsync();
            });

            //内容
            Content = new StackLayout
            {
                //间距
                Spacing = 10,
                //垂直方向上，从底部出发
                VerticalOptions = LayoutOptions.End,
                //堆放三个Label的方向是水平
                Orientation = StackOrientation.Horizontal,
                //水平方向上，从开始（左边）出发
                HorizontalOptions = LayoutOptions.Start,
                Children = { red, yellow, green, backButton }
            };
            #endregion
        }
    }
}