namespace Maui.Demo.Pages.Triggers;

public partial class StateTriggerPage : ContentPage
{
    public StateTriggerPage()
    {
        InitializeComponent();
    }

    private async void OnUncheckedStateIsActiveChanged(object sender, EventArgs e)
    {
        StateTriggerBase stateTrigger = sender as StateTriggerBase;
        Console.WriteLine($"Unchecked state active: {stateTrigger.IsActive}");
    }

    private void OnCheckedStateIsActiveChanged(object sender, EventArgs e)
    {
        StateTriggerBase stateTrigger = sender as StateTriggerBase;
        Console.WriteLine($"Checked state active: {stateTrigger.IsActive}");
    }
}