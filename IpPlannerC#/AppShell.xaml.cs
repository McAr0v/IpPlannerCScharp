using Firebase.Auth;
using IpPlannerC_.DataBase.Auth;
using IpPlannerC_.User;

namespace IpPlannerC_
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            
            InitializeComponent();

        }

        private async Task InitializeAppAsync()
        {
            FirebaseAuthClient? client = AuthInDataBase.getUserClient();

            if (client != null && client.User != null)
            {
                CustomUser user = CustomUser.GetEmptyUser();
                await user.GetUserFromDbAsync(client.User.Uid);


            }
        }
    }
}
