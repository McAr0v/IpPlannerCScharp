using Firebase.Database;
using Firebase.Database.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IpPlannerC_.DataBase.DbManager
{
    internal class DbManager
    {
        //FirebaseClient firebase = new FirebaseClient("https://ipplanner-4ca92-default-rtdb.firebaseio.com");
        FirebaseClient firebase = new FirebaseClient("https://ipplannercscharp-default-rtdb.firebaseio.com");

        private async Task PublishToDb<T>(T publishData, string path)
        {
            try
            {
                await firebase
                        .Child(path) // Указываем узел, куда хотим записать данные
                        .PutAsync(publishData); // Отправляем объект данных в базу данных Firebase

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }


        public static async void PublishInDb<T>(T publishData, string path)
        {

            DbManager dbManager = new DbManager();

            await dbManager.PublishToDb(publishData, path);
        }


        public static string GenerateKey()
        {
            Guid guid = Guid.NewGuid();
            return guid.ToString();
        }

        private async Task<IReadOnlyCollection<FirebaseObject<T>>?> GetDataFromDB<T>(string path)
        {
            try
            {
                // Получаем данные из узла "Test"
                var dataSnapshot = await firebase.Child(path).OnceAsync<T>();

                return dataSnapshot;

            }
            catch (Exception)
            {
                throw;
            }
        }

        public static async Task<IReadOnlyCollection<FirebaseObject<T>>?> GetFromDB<T>(string path)
        {

            DbManager dbManager = new DbManager();

            return await dbManager.GetDataFromDB<T>(path);
        }
    }
}
