namespace targv24_too;

public partial class FigurePage : ContentPage
{
	BoxView BoxView;
	Random random = new Random();
	HorizontalStackLayout hsl;
	public FigurePage()
	{
		int red = random.Next(0, 256);
		int green = random.Next(0, 256);
		int blue = random.Next(0, 256);
		BoxView = new BoxView
		{
			Color = Color.FromRgb(red, green, blue),
			WidthRequest = DeviceDisplay.MainDisplayInfo.Width / 4,
			HeightRequest = DeviceDisplay.MainDisplayInfo.Height / 4,
			CornerRadius = 20,
			HorizontalOptions = LayoutOptions.Center,
			VerticalOptions = LayoutOptions.Center,
			BackgroundColor = Color.FromRgba (0, 0, 0, 0)
		};
		TapGestureRecognizer tapGesture = new TapGestureRecognizer();
		tapGesture.Tapped += Klik_boksi_pael; //+= tab name for private void lower
		BoxView.GestureRecognizers.Add(tapGesture);
		hsl = new HorizontalStackLayout
		{
			Children = { BoxView },
			HorizontalOptions = LayoutOptions.Center,
			VerticalOptions = LayoutOptions.Center,

		};
		Content = hsl;
	}

    private void Klik_boksi_pael(object? sender, TappedEventArgs e)
    {
		BoxView.Color = Color.FromRgb(random.Next(0, 256), random.Next(0, 256), random.Next(0, 256));
		BoxView.WidthRequest = random.Next(50, (int)(DeviceDisplay.MainDisplayInfo.Width / 2));
        BoxView.HeightRequest = random.Next(50, (int)(DeviceDisplay.MainDisplayInfo.Height / 2));
		BoxView.CornerRadius = random.Next(0, 101);
    }
}
