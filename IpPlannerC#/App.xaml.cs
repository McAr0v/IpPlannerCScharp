using Firebase.Auth;
using IpPlannerC_.DataBase.Auth;
using IpPlannerC_.DataBase.DbManager;
using IpPlannerC_.Design.AuthScreens;
using IpPlannerC_.User;

namespace IpPlannerC_
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

                       

            //MainPage = new AppShell();

            FirebaseAuthClient? client = AuthInDataBase.getUserClient();

            var navigationPage = new NavigationPage(new SignInPage());

            NavigationPage.SetHasNavigationBar(navigationPage.CurrentPage, false);

            if (client != null && client.User != null)
            {

                MainPage = new AppShell();
            }
            else 
            { 
                MainPage = navigationPage;
            }

        }

        /*protected override async void OnStart()
        {
            base.OnStart();

            FirebaseAuthClient? client = AuthInDataBase.getUserClient();

            var navigationPage = new NavigationPage(new SignInPage());

            NavigationPage.SetHasNavigationBar(navigationPage.CurrentPage, false);

            if (client != null && client.User != null)
            {
                CustomUser user = CustomUser.GetEmptyUser();
                //await user.GetUserFromDbAsync(client.User.Uid);
                MainPage = new AppShell();
            }
            else
            {
                MainPage = navigationPage;
            }
        }*/

    }
}
