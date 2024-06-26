﻿using IpPlannerC_.DataBase.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IpPlannerC_.Design.AuthScreens
{
    public class SignInPage : ContentPage
    {

        Entry emailEntry = new Entry();
        Entry passwordEntry = new Entry();
        Button regButton = new Button();
        Button signInButton = new Button();

        public SignInPage()
        {
            regButton.Clicked += GoToReg;
            signInButton.Clicked += SignIn;
            emailEntry.Placeholder = "Email";
            emailEntry.Keyboard = Keyboard.Email;

            passwordEntry.Placeholder = "Пароль";
            passwordEntry.IsPassword = true;

            regButton.Text = "Зарегистрироваться";
            signInButton.Text = "Войти";
            
            Padding = new Thickness(20);

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
                                    Text = "Вход в аккаунт",
                                    TextColor = Colors.Black,
                                    FontSize = 30,
                                    FontAttributes = FontAttributes.Bold,
                                    HorizontalOptions = LayoutOptions.Center
                                },

                                new Label()
                                {
                                    Text = "Введите свои данные для входа",
                                    TextColor = Colors.Black,
                                    FontSize = 12,
                                    HorizontalOptions = LayoutOptions.Center,


                                }
                            }
                        },



                        emailEntry,
                        passwordEntry,
                        signInButton,

                        new StackLayout
                        {
                            Margin = new Thickness(0, 30),
                            Spacing = 20,

                            Children =
                            {
                                new Label()
                                {
                                    Text = "Нет аккаунта? Зарегистрируйтесь",
                                    TextColor = Colors.Black,
                                    FontSize = 12,
                                    HorizontalOptions = LayoutOptions.Center,

                                },

                                regButton
                            }
                        }


                    }
                }
            };

        }

        private async void SignIn(object? sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(emailEntry.Text) || string.IsNullOrWhiteSpace(passwordEntry.Text))
            {
                await DisplayAlert("Ошибка", "Пожалуйста, введите имя, Email и пароль.", "OK");
                return;
            }

            string result = await AuthInDataBase.SignIn(emailEntry.Text, passwordEntry.Text);

            if (result == "ok")
            {
                await DisplayAlert("Успех!", "Ты успешно вошел в аккаунт", "OK");
                Application.Current.MainPage = new AppShell();
            }
        }

        private async void GoToReg(object? sender, EventArgs e)
        {
            
            await Navigation.PushModalAsync(new RegisterScreen());
            
        }
    }
}
