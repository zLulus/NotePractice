namespace Maui.Demo.Pages.Bindings;

public partial class BindingModePage : ContentPage
{
    private double enteredNumber;
    public double EnteredNumber
    {
        get { return enteredNumber; }
        set
        {
            if (enteredNumber != value)
            {
                enteredNumber = value;
                OnPropertyChanged(nameof(EnteredNumber));
            }
        }
    }
    public BindingModePage()
    {
        InitializeComponent();
        EnteredNumber = 1.23;
        BindingContext = this;
    }


    private void OnTextChanged(object sender, TextChangedEventArgs e)
    {
        if (double.TryParse(e.NewTextValue, out double result))
        {
            EnteredNumber = result;
        }
    }

    private void OnResetClicked(object sender, EventArgs e)
    {
        EnteredNumber = 0;
        numberEntry.Text = "";
    }
}