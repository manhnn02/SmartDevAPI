using System;
using DAL.Models;
using Services.Helpers;

namespace Services.Models
{
    public class BookVM
    {
        public BookVM()
        {
        }

        public long BOOK_ID { get; set; }
        public string BOOK_NAME { get; set; }
        public string BOOK_DES { get; set; }
        public long USER_ID { get; set; }
        public bool? STATUS { get; set; }

        public UserVM USER { get; set; }
    }
}

