using ApiDevBP.Models;

namespace ApiDevBP.Services
{
    public interface IUserServices
    {

        bool SaveUser(UserModel user);
        IEnumerable<UserModel>? GetUsers();

        UserModel? GetUser(int id);

        bool DeleteUser(int id);

        bool EditUser(UserModel user,int id);
    }
}
