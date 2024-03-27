using Firebase.Database;
using Firebase.Database.Query;
using IpPlannerC_.DataBase.DbManager;
using Newtonsoft.Json;
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

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email address")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }

        

        [Required(ErrorMessage = "UID is required")]
        public string Uid { get; set; }

        public CustomUser(string email, string name,  string uid)
        {
            Email = email; 
            Name = name;
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
            FirebaseClient firebase = new FirebaseClient("https://ipplannercscharp-default-rtdb.firebaseio.com");

            var users = await firebase.Child(uid).Child("userInfo").OnceAsJsonAsync();

            CustomUser user = JsonConvert.DeserializeObject<CustomUser>(users);

            SetCurrentUserFromUser(user);

            return user;
        }

    }
}
