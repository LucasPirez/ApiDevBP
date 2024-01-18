using ApiDevBP.Configurations;
using ApiDevBP.Entities;
using ApiDevBP.Models;
using AutoMapper;
using SQLite;

namespace ApiDevBP.Services
{
    public class UserServices: IUserServices
    {
        private readonly SQLiteConnection _db;

        public IMapper _mapper;

        public UserServices(SQLiteConnection db,IMapper autoMapper)
        {
            _db = db;
            _db.CreateTable<UserEntity>();
            _mapper = autoMapper;
        }

        public  bool SaveUser(UserModel user)
        {
            var result =  _db.Insert(_mapper.Map<UserEntity>(user));

            return result > 0;
        }

        public IEnumerable<UserModel>? GetUsers()
        {
            IEnumerable<UserEntity> users =  _db.Query<UserEntity>($"Select * from Users");

            if (users == null) return null;

            return users.Select(user => _mapper.Map<UserModel>(user));
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

        public bool DeleteUser(int id)
        {
            var edit = _db.Execute($"DELETE from Users  WHERE Id = {id}");

            return edit > 0;
        }


        public UserModel? GetUser(int id)
        {
            var user = _db.Query<UserEntity>($"Select * from Users Where Id = {id}").FirstOrDefault();

            if (user == null) return null;

            return _mapper.Map<UserModel>(user);
        }
    }
}
