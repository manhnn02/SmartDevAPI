using System;
using Services.Helpers;

namespace Services.Models
{
    public class BookVM : DAL.Models.Book
    {
        public BookVM()
        {
            ResponseCode = ResponseCode.Success;
        }
        public ResponseCode ResponseCode { get; set; }
    }
}

