using ApiDevBP.Configurations;
using ApiDevBP.Entities;
using ApiDevBP.Models;
using SQLite;

namespace ApiDevBP.Services
{
    public class UserServices:IUserServices
    {
        private readonly SQLiteConnection _db;

        public UserServices(ILocalDatabase db)
        {
            _db = db.SQLiteConnection();
            _db.CreateTable<UserEntity>();
        }

        public  bool SaveUser(UserModel user)
        {
            var result =  _db.Insert(new UserEntity()
            {
                Name = user.Name,
                Lastname = user.Lastname
            });

            return result > 0;
        }

        public  IEnumerable<UserModel>? GetUsers()
        {
            var users =  _db.Query<UserEntity>($"Select * from Users");
            if (users == null) return null;
             return users.Select(x => new UserModel()
                {
                    Name = x.Name,
                    Lastname = x.Lastname
                  
                });
            

        }

        public bool DeleteUser( int id)
        {
         
            var edit = _db.Execute($"DELETE from Users  WHERE Id = {id}"
           );





            return edit > 0;
        }

        public bool EditUser(UserModel user,int id)
        {
            var userToEdit = _db.Query<UserEntity>($"Select * from Users Where Id = {id}").FirstOrDefault(); ;

            if (userToEdit == null) return false;

            userToEdit.Name = user.Name;
            userToEdit.Lastname = user.Lastname;
          

            var edit = _db.Update(userToEdit);


            return edit > 0;
        }

        public UserModel? GetUser(int id)
        {
            var user = _db.Query<UserEntity>($"Select * from Users Where Id = {id}").FirstOrDefault();

            if (user == null) return null;

            return new UserModel { Lastname = user.Lastname, Name = user.Name };
        }
    }
}
