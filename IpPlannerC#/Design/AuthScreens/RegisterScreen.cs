using IpPlannerC_.DataBase.Auth;
using IpPlannerC_.DataBase.DbManager;
using IpPlannerC_.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IpPlannerC_.Design.AuthScreens
{
    public class RegisterScreen : ContentPage
    {
        Entry emailEntry = new Entry();
        Entry nameEntry = new Entry();
        Entry passwordEntry = new Entry();
        Button regButton = new Button();
        Button signInButton = new Button();

        NavigationPage navigationPage = new NavigationPage();



        public RegisterScreen()
        {
            //this.navigationPage = navigationPage;

            regButton.Clicked += TryReg;
            signInButton.Clicked += GoToSignIn;

            emailEntry.Placeholder = "Email";
            emailEntry.Keyboard = Keyboard.Email;
            nameEntry.Placeholder = "Имя";
            nameEntry.Keyboard = Keyboard.Text;
            passwordEntry.Placeholder = "Пароль";
            passwordEntry.IsPassword = true;
            regButton.Text = "Зарегистрироваться";
            signInButton.Text = "Войти";
            //BackgroundColor = Colors.Black;
            Padding = new Thickness(20);
            Title = "Регистрация";

            Content = new ScrollView
            {
                Content = new StackLayout
                {
                    
                    Spacing = 20,
                    
                    Children =
                    {

                        new StackLayout
                        {
                            
                            Children =
                            {
                                new Label()
                                {
                                    Text = "Регистрация",
                                    TextColor = Colors.Black,
                                    FontSize = 30,
                                    FontAttributes = FontAttributes.Bold,
                                    HorizontalOptions = LayoutOptions.Center
                                },

                                new Label()
                                {
                                    Text = "Введите свои данные для регистрации",
                                    TextColor = Colors.Black,
                                    FontSize = 12,
                                    HorizontalOptions = LayoutOptions.Center,
                                    

                                }
                            }
                        },

                        

                        emailEntry,
                        nameEntry,
                        passwordEntry,
                        regButton,

                        new StackLayout
                        {
                            Margin = new Thickness(0, 30),
                            Spacing = 20,
                            
                            Children = 
                            {
                                new Label()
                                {
                                    Text = "Уже есть аккаунт? Войдите в него",
                                    TextColor = Colors.Black,
                                    FontSize = 12,
                                    HorizontalOptions = LayoutOptions.Center,

                                },

                                signInButton
                            }
                        }

                        
                    }
                }
            };
        }

        private async void TryReg(object? sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(emailEntry.Text) || string.IsNullOrWhiteSpace(passwordEntry.Text) || string.IsNullOrWhiteSpace(nameEntry.Text))
            {
                await DisplayAlert("Ошибка", "Пожалуйста, введите имя, Email и пароль.", "OK");
                return;
            }

            string result = await AuthInDataBase.RegisterUser(emailEntry.Text, passwordEntry.Text, nameEntry.Text);

            if (result == "ok") 
            {

                try 
                {
                    CustomUser user = CustomUser.GetCurrentUser();

                    DbManager.PublishInDb(user, $"{user.Uid}/userInfo");

                    await DisplayAlert("Отлично!", "Ты успешно зарегистрировался!", "OK");
                }
                catch (Exception ex)
                {
                    await DisplayAlert("Ошибка", ex.Message, "OK");
                }

                
            }

        }



        private async void GoToSignIn(object? sender, EventArgs e)
        {
            //await Navigation.PushAsync(new AppShell());
            Application.Current.MainPage = new AppShell();

        }
    }
}
