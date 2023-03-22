namespace Maui.Demo.Pages.Bindings;

/// <summary>
/// https://learn.microsoft.com/zh-cn/dotnet/maui/fundamentals/data-binding/basic-bindings?view=net-maui-7.0&WT.mc_id=DT-MVP-5003010
/// </summary>
public partial class BindingRotationAndScalePage : ContentPage
{
    public BindingRotationAndScalePage()
    {
        InitializeComponent();

        label1.BindingContext = slider1;
        label1.SetBinding(Label.RotationProperty, "Value");

        label3.SetBinding(Label.ScaleProperty, new Binding("Value", source: slider3));
    }
}