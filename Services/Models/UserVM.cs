using System;
using DAL.Models;
using Services.Helpers;

namespace Services.Models
{
    public class UserVM : DAL.Models.User
    {
        public UserVM()
        {
            ResponseCode = ResponseCode.Success;
        }
        public ResponseCode ResponseCode { get; set; }
    }
}

