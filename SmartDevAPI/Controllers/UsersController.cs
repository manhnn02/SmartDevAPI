using Microsoft.AspNetCore.Mvc;
using Services;
using Services.Helpers;
using Services.Models;

namespace SmartDevAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserServices _userServices;

        public UsersController(IUserServices userServices)
        {
            this._userServices = userServices;
        }

        // GET: api/Users
        [HttpGet]
        public ActionResult<APIResponse> GetAll()
        {
            try
            {
                return Ok(_userServices.GetAll());
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        //// GET: api/Users/5
        //[HttpGet("{id}")]
        //public ActionResult<UserVM> GetUser(long id)
        //{
        //    try
        //    {
        //        return _userServices.GetUser(id);
        //    }
        //    catch
        //    {
        //        return BadRequest();
        //    }
        //}

        //// PUT: api/Users/5
        //// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPut("{id}")]
        //public ActionResult<UserVM> PutUser(long id, UserVM user)
        //{
        //    if (id != user.UserId)
        //    {
        //        return BadRequest();
        //    }

        //    try
        //    {
        //        return _userServices.UpdateUser(user);
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!_userServices.UserExists(id, user.UserEmail))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }
        //}

        //// POST: api/Users
        //// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPost]
        //public ActionResult<UserVM> PostUser(UserVM user)
        //{
        //    try {
        //        return _userServices.AddUser(user);
        //    }
        //    catch (Exception ex) {
        //        return BadRequest();
        //    }
        //}

        //// DELETE: api/Users/5
        //[HttpDelete("{id}")]
        //public ActionResult DeleteUser(long id)
        //{
        //    try
        //    {
        //        return _userServices.DeleteUser(id) ? Ok() : BadRequest();
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest();
        //    }
        //}
    }
}

