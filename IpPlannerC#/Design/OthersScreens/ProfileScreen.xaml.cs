using IpPlannerC_.DataBase.Auth;
using IpPlannerC_.User;

namespace IpPlannerC_.Design.OthersScreens;

public partial class ProfileScreen : ContentPage
{

	Button getUser = new Button();
	Button signOutUser = new Button();

	Label label = new Label();

	CustomUser user = CustomUser.GetCurrentUser();

	public ProfileScreen()
	{
		InitializeComponent();

		//GetUserFromDb();

		getUser.Text = "�������� ������";
		getUser.Clicked += SignIn;

		signOutUser.Text = "�����";
		signOutUser.Command = new Command(async () =>
		{
			AuthInDataBase.SignOut();
			user = CustomUser.GetCurrentUser();
			await DisplayAlert("Success", "�� ������� �����!", "OK");

		});

		

		if (user == null) user = CustomUser.GetEmptyUser();
        label.Text = user.Name == "" ? "��� �����" : user.Name;


        Content = new StackLayout
		{
			Orientation = StackOrientation.Vertical,
			Children = 
			{
				label,

				getUser,
				signOutUser
			}
		};

    }

	private async void GetUserFromDb()
	{
        await user.GetUserFromDbAsync(AuthInDataBase.getUserClient().User.Uid);
        user = CustomUser.GetCurrentUser();
    }

    private async void SignIn(object? sender, EventArgs e)
    {
		await user.GetUserFromDbAsync(AuthInDataBase.getUserClient().User.Uid);
        user = CustomUser.GetCurrentUser();
        label.Text = user.Name;
    }
}