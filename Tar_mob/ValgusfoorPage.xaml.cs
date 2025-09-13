using System;
using System.Threading.Tasks;
using Microsoft.Maui.Controls;

namespace targv24_too;

public partial class ValgusfoorPage : ContentPage
{

    private Label pealkirjLabel;
    private Label staatusLabel;
    private Button sisseNupp;
    private Button valjaNepp;
    private Button paevaNupp;
    private Button ooNupp;
    private Frame valgusfoorFrame;
    private VerticalStackLayout valgusfoorStack;
    private List<Frame> tulukesed;
    private List<string> varvid = new List<string> { "punane", "kollane", "roheline" };
    private List<string> teated = new List<string> { "Seiska!", "Ootama!", "Sõida!" };

    // Olekumuutujad
    private bool onSisse = false;
    private bool onPaevaRezim = true;
    private int aktivneTuli = -1; // -1 = mitte ühtegi, 0 = punane, 1 = kollane, 2 = roheline
    private bool automaatneRezim = false;

    public ValgusfoorPage()
    {
        Title = "Valgusfoor";
        BackgroundImageSource = "dotnet_bot.png"; 

        LooLiides();
        Content = LooSisu();
    }

    private void LooLiides()
    {
        // Pealkiri
        pealkirjLabel = new Label
        {
            Text = "Liikluse Valgusfoor",
            FontSize = 24,
            FontAttributes = FontAttributes.Bold,
            TextColor = Colors.White,
            HorizontalOptions = LayoutOptions.Center,
            Margin = new Thickness(0, 20, 0, 10)
        };

        // Staatus 
        staatusLabel = new Label
        {
            Text = "Valgusfoor on välja lülitatud",
            FontSize = 16,
            TextColor = Colors.LightGray,
            HorizontalOptions = LayoutOptions.Center,
            Margin = new Thickness(0, 0, 0, 20)
        };

        // Nupud
        sisseNupp = new Button
        {
            Text = "Sisse",
            FontSize = 18,
            BackgroundColor = Color.FromRgb(34, 139, 34),
            TextColor = Colors.White,
            CornerRadius = 10,
            Margin = new Thickness(5)
        };
        sisseNupp.Clicked += SisseNupp_Clicked;

        valjaNepp = new Button
        {
            Text = "Välja",
            FontSize = 18,
            BackgroundColor = Color.FromRgb(220, 20, 60),
            TextColor = Colors.White,
            CornerRadius = 10,
            Margin = new Thickness(5)
        };
        valjaNepp.Clicked += ValjaNepp_Clicked;

        paevaNupp = new Button
        {
            Text = "Päeva režiim",
            FontSize = 14,
            BackgroundColor = Color.FromRgb(255, 165, 0),
            TextColor = Colors.White,
            CornerRadius = 8,
            Margin = new Thickness(5)
        };
        paevaNupp.Clicked += PaevaNupp_Clicked;

        ooNupp = new Button
        {
            Text = "Öö režiim",
            FontSize = 14,
            BackgroundColor = Color.FromRgb(25, 25, 112),
            TextColor = Colors.White,
            CornerRadius = 8,
            Margin = new Thickness(5)
        };
        ooNupp.Clicked += OoNupp_Clicked;
        LooValgusfoor();
    }

    private void LooValgusfoor()
    {
        tulukesed = new List<Frame>();
        valgusfoorStack = new VerticalStackLayout
        {
            Spacing = 10,
            HorizontalOptions = LayoutOptions.Center
        };
        for (int i = 0; i < 3; i++)
        {
            Frame tuli = new Frame
            {
                WidthRequest = 80,
                HeightRequest = 80,
                CornerRadius = 40,
                BorderColor = Colors.Black,
                HasShadow = true,
                BackgroundColor = Colors.Gray,
                HorizontalOptions = LayoutOptions.Center,
                Margin = new Thickness(5)
            };
            var tapGesture = new TapGestureRecognizer();
            int tuliIndex = i;
            tapGesture.Tapped += (sender, e) => TuliKlick(tuliIndex);
            tuli.GestureRecognizers.Add(tapGesture);

            tulukesed.Add(tuli);
            valgusfoorStack.Children.Add(tuli);
        }

        valgusfoorFrame = new Frame
        {
            BackgroundColor = Color.FromRgb(64, 64, 64),
            BorderColor = Colors.Black,
            CornerRadius = 15,
            Padding = new Thickness(20),
            HorizontalOptions = LayoutOptions.Center,
            Content = valgusfoorStack
        };
    }

    private ScrollView LooSisu()
    {
        var nuppudeRida1 = new HorizontalStackLayout
        {
            HorizontalOptions = LayoutOptions.Center,
            Children = { sisseNupp, valjaNepp }
        };

        var nuppudeRida2 = new HorizontalStackLayout
        {
            HorizontalOptions = LayoutOptions.Center,
            Children = { paevaNupp, ooNupp }
        };

        var peamineStack = new VerticalStackLayout
        {
            BackgroundColor = Color.FromRgba(0, 0, 0, 0.7),
            Padding = new Thickness(20),
            Spacing = 15,
            Children = {
                pealkirjLabel,
                staatusLabel,
                valgusfoorFrame,
                nuppudeRida1,
                nuppudeRida2
            }
        };

        return new ScrollView
        {
            Content = peamineStack,
            BackgroundColor = Color.FromRgb(30, 30, 30)
        };
    }

    private async void SisseNupp_Clicked(object sender, EventArgs e)
    {
        onSisse = true;
        staatusLabel.Text = onPaevaRezim ? "Valgusfoor töötab - Päeva režiim" : "Valgusfoor töötab - Öö režiim";
        staatusLabel.TextColor = Colors.LightGreen;

        if (!onPaevaRezim)
        {
            automaatneRezim = true;
            _ = AutomaatneOoRezim(); 
        }
    }

    private void ValjaNepp_Clicked(object sender, EventArgs e)
    {
        onSisse = false;
        automaatneRezim = false;
        aktivneTuli = -1;
        staatusLabel.Text = "Valgusfoor on välja lülitatud";
        staatusLabel.TextColor = Colors.LightGray;
        foreach (var tuli in tulukesed)
        {
            tuli.BackgroundColor = Colors.Gray;
            tuli.BorderColor = Colors.Black;
            NormaalneEffekt(tuli);
        }
    }

    private void PaevaNupp_Clicked(object sender, EventArgs e)
    {
        onPaevaRezim = true;
        automaatneRezim = false;
        if (onSisse)
        {
            staatusLabel.Text = "Valgusfoor töötab - Päeva režiim";
            foreach (var tuli in tulukesed)
            {
                tuli.BackgroundColor = Colors.Gray;
                NormaalneEffekt(tuli);
            }
            aktivneTuli = -1;
        }
    }

    private async void OoNupp_Clicked(object sender, EventArgs e)
    {
        onPaevaRezim = false;
        if (onSisse)
        {
            staatusLabel.Text = "Valgusfoor töötab - Öö režiim";
            automaatneRezim = true;
            _ = AutomaatneOoRezim();
        }
    }

    private async Task AutomaatneOoRezim()
    {
        while (automaatneRezim && !onPaevaRezim && onSisse)
        {
            tulukesed[1].BackgroundColor = Colors.Yellow;
            AktiivneEffekt(tulukesed[1]);
            await Task.Delay(800);

            if (!automaatneRezim) break;

            tulukesed[1].BackgroundColor = Colors.Gray;
            NormaalneEffekt(tulukesed[1]);
            await Task.Delay(800);
        }
    }

    private void TuliKlick(int tuliIndex)
    {
        if (!onSisse)
        {
            DisplayAlert("Hoiatus", "Kõigepealt lülita valgusfoor sisse!", "OK");
            return;
        }

        if (!onPaevaRezim)
        {
            DisplayAlert("Info", "Öö režiimis töötab valgusfoor automaatselt", "OK");
            return;
        }

        foreach (var tuli in tulukesed)
        {
            tuli.BackgroundColor = Colors.Gray;
            NormaalneEffekt(tuli);
        }

        // Aktiveerime klõpsatud tule
        aktivneTuli = tuliIndex;
        Color tuliVarv = Colors.Gray;
        string teade = "";

        switch (tuliIndex)
        {
            case 0: // Punane
                tuliVarv = Colors.Red;
                teade = "Seiska!";
                break;
            case 1: // Kollane
                tuliVarv = Colors.Yellow;
                teade = "Ootama!";
                break;
            case 2: // Roheline
                tuliVarv = Colors.Green;
                teade = "Sõida!";
                break;
        }

        tulukesed[tuliIndex].BackgroundColor = tuliVarv;
        AktiivneEffekt(tulukesed[tuliIndex]);
        DisplayAlert("Valgusfoor", teade, "OK");
    }

    private void AktiivneEffekt(Frame tuli)
    {
        tuli.Scale = 1.1;
        tuli.BorderColor = Colors.White;
        tuli.HasShadow = true;
    }

    private void NormaalneEffekt(Frame tuli)
    {
        tuli.Scale = 1.0;
        tuli.BorderColor = Colors.Black;
        tuli.HasShadow = false;
    }
}