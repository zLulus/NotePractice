using Maui.Demo.Pages.Formats.ViewModels;
using System.Collections.ObjectModel;

namespace Maui.Demo.Pages.Formats;

/// <summary>
/// https://learn.microsoft.com/zh-cn/dotnet/maui/fundamentals/data-binding/string-formatting?view=net-maui-7.0&WT.mc_id=DT-MVP-5003010
/// </summary>
public partial class StringFormatPage : ContentPage
{
    public StringFormatPage()
    {
        InitializeComponent();

        BindingContext = new StringFormatPageViewModel()
        {
            DisplayDateTime = DateTime.Now,
            Number = (decimal)123.4567,
            Price = (decimal)543.21098,
            Score = (decimal)675.342,
            Products = new ObservableCollection<ProductViewModel>()
            {
                new ProductViewModel()
                {
                    Name="Product 1",
                    Price=(decimal)432.876,
                    Stock=10,
                    InStock=2
                },
                 new ProductViewModel()
                {
                    Name="Product 2",
                    Price=(decimal)78665.234,
                    Stock=20,
                    InStock=3
                },
            }
        };
    }
}