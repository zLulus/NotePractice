namespace Maui.Demo.Pages.Bindings;

public partial class PropertyBindingPage : ContentPage
{
    public PropertyBindingPage()
    {
        InitializeComponent();

        label1.BindingContext = slider1;
        label1.SetBinding(Label.RotationProperty, "Value");

        label3.SetBinding(Label.ScaleProperty, new Binding("Value", source: slider3));
    }
}