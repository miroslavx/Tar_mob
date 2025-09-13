namespace targv24_too;

public partial class TimerPage : ContentPage
{
    public TimerPage()
    {
        Title = "Timer leht";

        var label = new Label
        {
            Text = "Timer leht tuleb varsti!",
            FontSize = 18,
            HorizontalOptions = LayoutOptions.Center,
            VerticalOptions = LayoutOptions.Center
        };

        Content = new StackLayout
        {
            Children = { label }
        };
    }
}