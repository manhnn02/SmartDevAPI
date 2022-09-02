using System;
namespace Services.Helpers
{
    public enum ResponseCode
    {
        Success = 200, Error = 500, BadRequest = 400, Created = 201
    }
    public enum BookStatus
    {
        UnRead = 0, Read = 1
    }
}
