using Services.Helpers;
using Services.Models;

namespace Services;
public interface IUserServices
{
    List<UserVM> GetAll();
    UserVM GetUser(long uID);
    UserVM AddUser(UserVM user);
    UserVM UpdateUser(UserVM user);
    bool DeleteUser(long uID);
    bool UserExists(long id, string email);
}