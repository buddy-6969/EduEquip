namespace EduEquip
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            // Set the main page to a NavigationPage wrapping your first page
            MainPage = new NavigationPage(new splash());
        }
    }
}