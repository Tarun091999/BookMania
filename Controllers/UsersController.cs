using BookManagement.BLL.Services;
using BookManagement.Entity.ModelDTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        public IUserServices _userService;

        public UsersController(IUserServices userService)
        {
            _userService = userService;
        }

        [HttpPost("signup")]
        public async Task <IActionResult>Signup(UserDTO userObj)
        {
          if ( await _userService.Signup(userObj))
          {
                return Ok("User Added");
          }

            return BadRequest("User Already Exist");
          
        }


        [HttpPost("login")]
        public async Task<IActionResult> Login (LoginDTO userObj)
        {
            if (await _userService.Login(userObj))
            {

                return Ok("Login Successfully ");
            }

            return BadRequest("Something Went Wrong");

        }


       
    }
}
