using Maui.Demo.Pages.Bindings.ViewModels;

namespace Maui.Demo.Pages.Bindings;

public partial class DataConversionPage : ContentPage
{
	public DataConversionPage()
	{
		InitializeComponent();

		BindingContext = new SliderValueViewModel();
	}
}