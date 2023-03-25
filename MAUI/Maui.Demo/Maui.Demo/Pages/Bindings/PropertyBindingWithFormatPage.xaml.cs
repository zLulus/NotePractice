namespace Maui.Demo.Pages.Bindings;

public partial class PropertyBindingWithFormatPage : ContentPage
{
	public PropertyBindingWithFormatPage()
	{
		InitializeComponent();

		BindingContext = new SliderValueViewModel();
	}
}