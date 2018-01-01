using Android.Renderscripts;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace XamarinDemo.CustomizingCell.Cells
{
    public class EmployeeCell : ViewCell
    {
        public EmployeeCell()
        {
            var image = new Image
            {
                HorizontalOptions = LayoutOptions.Start
            };
            image.SetBinding(Image.SourceProperty, new Binding("ImageUri"));
            //设置宽高为40
            image.WidthRequest = image.HeightRequest = 40;

            var nameLayout = CreateNameLayout();
            var viewLayout = new StackLayout()
            {
                Orientation = StackOrientation.Horizontal,
                //加入图片、名称、推特
                Children = { image, nameLayout }
            };
            //把布局赋值给View
            View = viewLayout;
        }

        static StackLayout CreateNameLayout()
        {
            //新增Label
            var nameLabel = new Label
            {
                HorizontalOptions = LayoutOptions.FillAndExpand
            };
            //绑定Employee的DisplayName属性
            nameLabel.SetBinding(Label.TextProperty, "DisplayName");

            var twitterLabel = new Label
            {
                HorizontalOptions = LayoutOptions.FillAndExpand
            };
            twitterLabel.SetBinding(Label.TextProperty, "Twitter");

            var nameLayout = new StackLayout()
            {
                HorizontalOptions = LayoutOptions.StartAndExpand,
                //设置为从上到下排列
                Orientation = StackOrientation.Vertical,
                //将两个Label依次添加到Children中
                Children = { nameLabel, twitterLabel }
            };
            return nameLayout;
        }
    }
}
