using Microsoft.Maui.Layouts;

namespace targv24_too;

public partial class LumememmPage : ContentPage
{
    private AbsoluteLayout absoluteLayout;
    private Frame amberFrame;
    private Frame peaFrame;
    private Frame kehaFrame;
    private Picker tegevusePicker;
    private Button kaivitaButton;
    private Label staatusLabel;
    private Slider labipaistuvusSlider;
    private Stepper kiiruseStepper;

    private Random random = new Random();
    private List<string> tegevused = new List<string>
    {
        "Peida lumememm",
        "Näita lumememm",
        "Muuda värvi",
        "Sulata",
        "Tantsi"
    };

    public LumememmPage()
    {
        Title = "Lumememm";
        BackgroundColor = Color.FromRgb(173, 216, 230);

        LooLiides();
        Content = absoluteLayout;
    }

    private void LooLiides()
    {
        absoluteLayout = new AbsoluteLayout
        {
            BackgroundColor = Color.FromRgb(173, 216, 230)
        };

        LooLumememm();
        LooJuhtimised();
    }

    private void LooLumememm()
    {
        amberFrame = new Frame
        {
            BackgroundColor = Color.FromRgb(139, 69, 19),
            CornerRadius = 5,
            BorderColor = Colors.Black,
            WidthRequest = 80,
            HeightRequest = 60
        };

        kehaFrame = new Frame
        {
            BackgroundColor = Colors.White,
            CornerRadius = 50,
            BorderColor = Colors.Black,
            WidthRequest = 100,
            HeightRequest = 100
        };

        peaFrame = new Frame
        {
            BackgroundColor = Colors.White,
            CornerRadius = 35,
            BorderColor = Colors.Black,
            WidthRequest = 70,
            HeightRequest = 70
        };

        AbsoluteLayout.SetLayoutBounds(amberFrame, new Rect(0.5, 0.8, -1, -1));
        AbsoluteLayout.SetLayoutFlags(amberFrame, AbsoluteLayoutFlags.PositionProportional);

        AbsoluteLayout.SetLayoutBounds(kehaFrame, new Rect(0.5, 0.6, -1, -1));
        AbsoluteLayout.SetLayoutFlags(kehaFrame, AbsoluteLayoutFlags.PositionProportional);

        AbsoluteLayout.SetLayoutBounds(peaFrame, new Rect(0.5, 0.4, -1, -1));
        AbsoluteLayout.SetLayoutFlags(peaFrame, AbsoluteLayoutFlags.PositionProportional);

        absoluteLayout.Children.Add(amberFrame);
        absoluteLayout.Children.Add(kehaFrame);
        absoluteLayout.Children.Add(peaFrame);
    }

    private void LooJuhtimised()
    {
        tegevusePicker = new Picker
        {
            Title = "Vali tegevus",
            ItemsSource = tegevused,
            BackgroundColor = Colors.White,
            TextColor = Colors.Black
        };
        tegevusePicker.SelectedIndexChanged += TegevusePicker_SelectedIndexChanged;

        kaivitaButton = new Button
        {
            Text = "Käivita tegevus",
            BackgroundColor = Color.FromRgb(70, 130, 180),
            TextColor = Colors.White,
            CornerRadius = 10
        };
        kaivitaButton.Clicked += KaivitaButton_Clicked;

        staatusLabel = new Label
        {
            Text = "Vali tegevus",
            FontSize = 16,
            TextColor = Colors.Black,
            BackgroundColor = Colors.White,
            HorizontalTextAlignment = TextAlignment.Center,
            Padding = new Thickness(10, 5)
        };

        labipaistuvusSlider = new Slider
        {
            Minimum = 0.0,
            Maximum = 1.0,
            Value = 1.0,
            BackgroundColor = Colors.White
        };
        labipaistuvusSlider.ValueChanged += LabipaistuvusSlider_ValueChanged;

        kiiruseStepper = new Stepper
        {
            Minimum = 500,
            Maximum = 3000,
            Value = 1000,
            Increment = 250,
            BackgroundColor = Colors.White
        };

        var juhtnupudeStack = new VerticalStackLayout
        {
            Spacing = 10,
            Padding = new Thickness(20),
            Children = {
                new Label { Text = "Tegevus:", FontSize = 14, TextColor = Colors.Black },
                tegevusePicker,
                kaivitaButton,
                staatusLabel,
                new Label { Text = "Läbipaistvus:", FontSize = 14, TextColor = Colors.Black },
                labipaistuvusSlider,
                new Label { Text = "Kiirus:", FontSize = 14, TextColor = Colors.Black },
                kiiruseStepper
            }
        };

        AbsoluteLayout.SetLayoutBounds(juhtnupudeStack, new Rect(0, 0, 1, 0.3));
        AbsoluteLayout.SetLayoutFlags(juhtnupudeStack, AbsoluteLayoutFlags.All);

        absoluteLayout.Children.Add(juhtnupudeStack);
    }

    private void TegevusePicker_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (tegevusePicker.SelectedItem != null)
        {
            staatusLabel.Text = $"Valitud: {tegevusePicker.SelectedItem}";
        }
    }

    private async void KaivitaButton_Clicked(object sender, EventArgs e)
    {
        if (tegevusePicker.SelectedItem == null)
        {
            await DisplayAlert("Viga", "Palun vali tegevus!", "OK");
            return;
        }

        string valitudTegevus = tegevusePicker.SelectedItem.ToString();
        int kiirus = (int)kiiruseStepper.Value;

        switch (valitudTegevus)
        {
            case "Peida lumememm":
                await PeidaLumememm();
                break;
            case "Näita lumememm":
                await NaitaLumememm();
                break;
            case "Muuda värvi":
                await MuudaVarvi();
                break;
            case "Sulata":
                await SulataLumememm(kiirus);
                break;
            case "Tantsi":
                await TantsiLumememm(kiirus);
                break;
        }
    }

    private void LabipaistuvusSlider_ValueChanged(object sender, ValueChangedEventArgs e)
    {
        double opacity = e.NewValue;
        amberFrame.Opacity = opacity;
        kehaFrame.Opacity = opacity;
        peaFrame.Opacity = opacity;
    }

    private async Task PeidaLumememm()
    {
        staatusLabel.Text = "Lumememm on peidus";
        amberFrame.IsVisible = false;
        kehaFrame.IsVisible = false;
        peaFrame.IsVisible = false;
    }

    private async Task NaitaLumememm()
    {
        staatusLabel.Text = "Lumememm on nähtav";
        amberFrame.IsVisible = true;
        kehaFrame.IsVisible = true;
        peaFrame.IsVisible = true;
        amberFrame.Opacity = labipaistuvusSlider.Value;
        kehaFrame.Opacity = labipaistuvusSlider.Value;
        peaFrame.Opacity = labipaistuvusSlider.Value;
    }

    private async Task MuudaVarvi()
    {
        bool vastus = await DisplayAlert("Kinnitus", "Kas muuta lumememme värvi?", "Jah", "Ei");
        if (vastus)
        {
            Color uusVarv = Color.FromRgb(random.Next(0, 256), random.Next(0, 256), random.Next(0, 256));
            kehaFrame.BackgroundColor = uusVarv;
            peaFrame.BackgroundColor = uusVarv;
            staatusLabel.Text = "Värv muudetud!";
        }
        else
        {
            staatusLabel.Text = "Värvi ei muudetud";
        }
    }

    private async Task SulataLumememm(int kiirus)
    {
        staatusLabel.Text = "Lumememm sulab...";

        await Task.WhenAll(
            amberFrame.FadeTo(0, (uint)kiirus),
            kehaFrame.FadeTo(0, (uint)kiirus),
            peaFrame.FadeTo(0, (uint)kiirus),
            amberFrame.ScaleTo(0.1, (uint)kiirus),
            kehaFrame.ScaleTo(0.1, (uint)kiirus),
            peaFrame.ScaleTo(0.1, (uint)kiirus)
        );

        staatusLabel.Text = "Lumememm on sulanud";

        await Task.Delay(1000);
        amberFrame.Scale = 1.0;
        kehaFrame.Scale = 1.0;
        peaFrame.Scale = 1.0;
        amberFrame.Opacity = labipaistuvusSlider.Value;
        kehaFrame.Opacity = labipaistuvusSlider.Value;
        peaFrame.Opacity = labipaistuvusSlider.Value;
        staatusLabel.Text = "Lumememm on tagasi!";
    }

    private async Task TantsiLumememm(int kiirus)
    {
        staatusLabel.Text = "Lumememm tantsib!";

        for (int i = 0; i < 3; i++)
        {
            await Task.WhenAll(
                amberFrame.TranslateTo(50, 0, (uint)(kiirus / 2)),
                kehaFrame.TranslateTo(50, 0, (uint)(kiirus / 2)),
                peaFrame.TranslateTo(50, 0, (uint)(kiirus / 2))
            );

            await Task.WhenAll(
                amberFrame.TranslateTo(-50, 0, (uint)(kiirus / 2)),
                kehaFrame.TranslateTo(-50, 0, (uint)(kiirus / 2)),
                peaFrame.TranslateTo(-50, 0, (uint)(kiirus / 2))
            );
        }

        await Task.WhenAll(
            amberFrame.TranslateTo(0, 0, (uint)(kiirus / 2)),
            kehaFrame.TranslateTo(0, 0, (uint)(kiirus / 2)),
            peaFrame.TranslateTo(0, 0, (uint)(kiirus / 2))
        );

        staatusLabel.Text = "Tantsimine lõppenud!";
    }
}