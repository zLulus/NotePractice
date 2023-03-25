namespace Maui.Demo.Pages.Layouts;

public partial class GridByCodePage : ContentPage
{
    public GridByCodePage()
    {
        InitializeComponent();

        var grid = new Grid();

        //定义两个列，第一个列宽度为1*，第二个列宽度为2*
        grid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Star) });
        grid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(2, GridUnitType.Star) });

        //定义两个行，第一个行高度为1*，第二个行高度为2*
        grid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(1, GridUnitType.Star) });
        grid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(2, GridUnitType.Star) });

        //添加四个单元格
        grid.Add(new Label() { Text = "第一行第一列", BackgroundColor = Color.FromRgb(240, 230, 130) }, 0, 0); //第一行第一列
        grid.Add(new Label() { Text = "第一行第二列", BackgroundColor = Color.FromRgb(238, 232, 170) }, 1, 0); //第一行第二列
        grid.Add(new Label() { Text = "第二行第一列", BackgroundColor = Color.FromRgb(255, 218, 185) }, 0, 1); //第二行第一列
        grid.Add(new Label() { Text = "第二行第二列", BackgroundColor = Color.FromRgb(255, 228, 181) }, 1, 1); //第二行第二列

        Content = grid;
    }
}