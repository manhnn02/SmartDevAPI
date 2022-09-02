using System;
using DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace DAL.Implement
{
    public class UserRepository : IUserRepository
    {
        private readonly MyDbContext _context;
        public UserRepository(MyDbContext context)
        {
            this._context = context;
        }

        public User AddUser(User user)
        {
            _context.Add(user);
            _context.SaveChanges();
            return user;
        }

        public bool DeleteUser(long uID)
        {
            var user = GetUser(uID);
            if (user != null)
            {
                _context.Remove(user);
                _context.SaveChanges();

                return true;
            }
            return false;
        }

        public List<User> GetAll()
        {
            return _context.Users.ToList();
        }

        public User GetUser(long uID)
        {
            if(_context.Users != null)
                return _context.Users.SingleOrDefault(x => x.UserId == uID);
            else
                return null;
        }

        public User GetUserByEmail(string email)
        {
            if (_context.Users != null)
                return _context.Users.SingleOrDefault(x => x.UserEmail == email);
            else
                return null;
        }

        public User UpdateUser(User user)
        {
            var _user = GetUser(user.UserId);
            if (_user != null)
            {
                _user.UserName = user.UserName;
                _user.UserEmail = user.UserEmail;
                _user.UserPass = user.UserPass;
                _context.SaveChanges();

                return _user;
            }

            return null;
        }

        public bool UserExists(long id, string email)
        {
            return (_context.Users?.Any(e => e.UserId == id || e.UserEmail == email)).GetValueOrDefault();
        }

        public User Login(string email, string password)
        {
            if (_context.Users != null)
                return _context.Users.SingleOrDefault(x => x.UserEmail == email && x.UserPass == password);
            else
                return null;
        }
    }
}