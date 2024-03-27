using Firebase.Auth.Providers;
using Firebase.Auth.Repository;
using Firebase.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IpPlannerC_.User;

namespace IpPlannerC_.DataBase.Auth
{
    internal class AuthInDataBase
    {
        // Конфигурация подключения к сервису авторизации

        private static readonly FirebaseAuthConfig config = new FirebaseAuthConfig
        {
            //ApiKey = "AIzaSyARoqME43rFRS4ZgfYzwJD9W8KToFdtg40",
            ApiKey = "AIzaSyBLMSMLwtNWYhWK2EBV1PYlinyIk2zbjT4",
            //AuthDomain = "ipplanner-4ca92.firebaseapp.com",
            AuthDomain = "ipplannercscharp.firebaseapp.com",
            Providers = new FirebaseAuthProvider[]
                {
                    new EmailProvider()
                },
            // Локальная директория для сохранения данных о пользователе
            UserRepository = new FileUserRepository("UserData") // Persist data into %AppData%\FirebaseSample
        };

        // Клиент сервиса авторизации
        private static readonly FirebaseAuthClient client = new FirebaseAuthClient(config);

        // Функция, заполняющая Current User при инициализации приложения
        public static CustomUser? setUser()
        {
            if (client.User != null)
            {
                return new CustomUser
                (
                    name: client.User.Info.FirstName,
                    email: client.User.Info.Email,
                    uid: client.User.Info.Uid
                );
            }
            else return null;
        }


        public static async Task<string> RegisterUser(String email, String password, string name)
        {

            try
            {
                var userCredential = await client.CreateUserWithEmailAndPasswordAsync(email, password);

                var user = userCredential.User;
                var uidUser = user.Uid;

                // Проверяем успешность регистрации
                if (uidUser != null && user != null)
                {
                    CustomUser.SetCurrentUserFromUser(tempUser: new CustomUser(name: name, email: email, uid: uidUser));
                    return "ok";
                }
                else
                {
                    return "error";
                }
            }
            catch (Exception ex)
            {
                return ex.Message;

            }
        }

        // Функция выхода из профиля
        public static void SignOut()
        {

            client.SignOut();
            CustomUser.ClearUser();

        }

        // Функция входа в профиль

        public static async Task<string> SignIn(string email, string password)
        {

            Console.WriteLine($"------------------------ Начался вход ---------");

            try
            {
                var userCredential = await client.SignInWithEmailAndPasswordAsync(email, password);

                // Сохранение токена в безопасном хранилище

                var user = userCredential.User;
                var uidUser = user.Uid;

                // Проверяем успешность регистрации
                if (uidUser != null && user != null)
                {

                    Console.WriteLine($"------------------------UID из клиента {user.Uid}---------");

                    CustomUser customUser = CustomUser.GetEmptyUser();

                    Console.WriteLine($"------------------------Дошли до получения с БД---------");

                    customUser = await customUser.GetUserFromDbAsync(user.Uid);

                    //CustomUser.SetCurrentUserFromUser(customUser);

                    //CustomUser.SetCurrentUser(firstName: customUser.Uid, lastName: , email: user.Info.Email, uid: uidUser, phone: "", organizationName: "", gender: "");

                    return "ok";
                }
                else
                {
                    return "error";
                }
            }
            catch (Exception ex)
            {
                return ex.Message;

            }
        }
    }
}
