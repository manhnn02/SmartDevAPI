using Services.Helpers;
using Services.Models;

namespace Services;
public interface IUserServices
{
    APIResponse GetAll();
    APIResponse GetUser(long uID);
    APIResponse AddUser(UserVM user);
    APIResponse UpdateUser(UserVM user);
    APIResponse DeleteUser(long uID);
    bool UserExists(long id, string email);

    APIResponse Login(LoginModel model, string secretKey);
}