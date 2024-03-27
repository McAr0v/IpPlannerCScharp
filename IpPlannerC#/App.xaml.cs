using IpPlannerC_.Design.AuthScreens;

namespace IpPlannerC_
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            //MainPage = new AppShell();

            var navigationPage = new NavigationPage(new SignInPage());

            NavigationPage.SetHasNavigationBar(navigationPage.CurrentPage, false );

            MainPage = navigationPage;
        }
    }
}
