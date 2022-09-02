using DAL.Models;

namespace DAL
{
    public interface IUserRepository
    {
        List<User> GetAll();
        User GetUser(long uID);
        User GetUserByEmail(string email);
        User AddUser(User user);
        User UpdateUser(User user);
        bool DeleteUser(long uID);
        bool UserExists(long id, string email);
        User Login(string email, string password);
    }
}