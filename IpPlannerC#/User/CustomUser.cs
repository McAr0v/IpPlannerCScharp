using IpPlannerC_.DataBase.DbManager;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IpPlannerC_.User
{
    internal class CustomUser
    {

        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email address")]
        public string Email { get; set; }

        [Required(ErrorMessage = "UID is required")]
        public string Uid { get; set; }

        public CustomUser(string name, string email, string uid)
        {
            Name = name;
            Email = email;
            Uid = uid;
        }

        public static CustomUser? CurrentUser { get; private set; }

        public static void ClearUser()
        {
            CurrentUser = null;
        }

        public static void SetCurrentUserFromUser(CustomUser tempUser)
        {
            CurrentUser = tempUser;
        }

        public static CustomUser GetEmptyUser() 
        {
            return new CustomUser
                    (
                        uid: "",
                        name: "",
                        email: ""
                    );
        }

        public static CustomUser? GetCurrentUser() { return CurrentUser; }

        public async Task<CustomUser> GetUserFromDbAsync(string uid)
        {

            CustomUser user;

            var dataSnapshot = await DbManager.GetFromDB<CustomUser>($"{uid}/userInfo");

            if (dataSnapshot != null)
            {
                // Преобразовываем dataSnapshot в словарь (если это словарь)
                var userData = dataSnapshot as Dictionary<string, object>;

                // Создаем новый объект CustomUser и заполняем его данными из userData
                user = new CustomUser(
                    uid: userData.ContainsKey("Uid") ? userData["Uid"].ToString() : "",
                    name: userData.ContainsKey("Name") ? userData["Name"].ToString() : "",
                    email: userData.ContainsKey("Email") ? userData["Email"].ToString() : ""
                );

                SetCurrentUserFromUser(user);

                return user;

            }

            return GetEmptyUser();
        }

    }
}
