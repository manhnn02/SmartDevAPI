using System;
using DAL.Models;
using Services.Helpers;

namespace Services.Models
{
    public class UserVM
    {
        public UserVM()
        {
            BOOKS = new List<BookVM>();
        }
        public long USER_ID { get; set; }
        public string USER_NAME { get; set; }
        public string USER_EMAIL { get; set; }
        public string USER_PASS { get; set; }

        public ICollection<BookVM> BOOKS { get; set; }
    }
}

