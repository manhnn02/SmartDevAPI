using System;
namespace DAL.Models
{
    public class Book
    {
        public long BookId { get; set; }
        public string BookName { get; set; }
        public string BookDescription { get; set; }
        public long UserId { get; set; }
        public bool? Status { get; set; }

        public User User { get; set; }
    }
}

