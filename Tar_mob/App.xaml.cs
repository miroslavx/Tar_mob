namespace targv24_too
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
           NavigationPage MainPage = new NavigationPage(new AppShell());
        }

        protected override Window CreateWindow(IActivationState? activationState)
        {
            return new Window(new AppShell());
        }
    }
}