using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Support_API.Data;
using Support_API.Models.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Threading.Tasks;

namespace Support_API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        public readonly IUserManager myUserManager;
        public AuthController(IUserManager userManager)
        {
            myUserManager = userManager;
        }

        [HttpGet("Test")]
        public IActionResult GetTest()
        {
            User user = new User(myUserManager.CurrentUser, false);

            return Ok(user);
        }

        [HttpPost("Create")]
        public IActionResult CreateUser(string First_Name, string Middle_Name, string Last_Name, string Login, string Password)
        {
            CreateUserResponse crResponse = myUserManager.CreateUser(First_Name, Middle_Name, Last_Name, Login, Password);

            return Ok(crResponse);
        }

        [HttpPost("Login")]
        public IActionResult LoginUser(string Login, string Password)
        {
            AuthUserResponse authResponse = myUserManager.AuthenticateUser(Login, Password);

            return Ok(authResponse);
        }
    }
}
