namespace targv24_too;

public partial class TextPage : ContentPage  
{
    Label lblTekst;
    Editor editorTekst;
    HorizontalStackLayout hsl;

    public TextPage()
    {
        Title = "Teksti leht";  

        lblTekst = new Label()
        {
            Text = "Tekst: ",
            FontSize = 20,
            TextColor = Colors.Black,
            FontFamily = "Lovin Kites 400"
        };

        editorTekst = new Editor
        {
            FontSize = 20,
            BackgroundColor = Color.FromRgb(200, 200, 100),
            TextColor = Colors.Black,
            FontFamily = "Lovin Kites 400",
            AutoSize = EditorAutoSizeOption.TextChanges,
            Placeholder = "Siia tuleb tekst",
            PlaceholderColor = Colors.Gray,
            FontAttributes = FontAttributes.Italic
        };

        editorTekst.TextChanged += EditorTekst_TextChanged;

        hsl = new HorizontalStackLayout
        {
            BackgroundColor = Color.FromRgb(120, 30, 50),
            Children = { lblTekst, editorTekst },
            HorizontalOptions = LayoutOptions.Center,
            Padding = new Thickness(10)
        };

        Content = hsl;
    }

    private void EditorTekst_TextChanged(object? sender, TextChangedEventArgs e) 
    {
        lblTekst.Text = editorTekst.Text;
    }
}