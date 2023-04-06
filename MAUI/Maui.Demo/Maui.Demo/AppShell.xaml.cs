using Maui.Demo.Pages.Triggers;

namespace Maui.Demo;

/// <summary>
/// https://learn.microsoft.com/zh-cn/dotnet/maui/fundamentals/shell/?view=net-maui-7.0&WT.mc_id=DT-MVP-5003010
/// </summary>
public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();

        Routing.RegisterRoute(nameof(MultiTriggersPage), typeof(MultiTriggersPage));
        Routing.RegisterRoute(nameof(MainPage), typeof(MainPage));
    }
}
