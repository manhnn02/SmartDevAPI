using System;
namespace DAL.Models
{
    public class User
    {
        public User()
        {
            Books = new List<Book>();
        }

        public long UserId { get; set; }
        public string UserName { get; set; }
        public string UserEmail { get; set; }
        public string UserPass { get; set; }

        public ICollection<Book> Books { get; set; }
    }
}

