using System.Windows.Input;

namespace Maui.Demo.Pages.Bindings;

public partial class CommandBindingPage : ContentPage
{
    public ICommand ShowAlertCommand { get; set; }
    public CommandBindingPage()
    {
        InitializeComponent();

        ShowAlertCommand = new Command(() =>
        {
            DisplayAlert("Alert", "You clicked the button!", "OK");
        }, () => true);

        BindingContext = this;
    }
}