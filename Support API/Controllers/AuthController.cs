using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Support_API.Data;
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
            return Ok("Hello World!");
        }

        [HttpPost("Create")]
        public IActionResult CreateUser(string First_Name, string? Middle_Name, string Last_Name, string Login, string Password)
        {
            var user = myUserManager.CreateUser(First_Name, Middle_Name, Last_Name, Login, Password);

            if (user != null)
                return Ok(user);
            else
                return NoContent();
        }

        [HttpPost("Login")]
        public IActionResult LoginUser(string Login, string Password)
        {
            var token = myUserManager.AuthenticateUser(Login, Password);

            if (token != null)
                return Ok(new { Login = Login, Success = true, JWT = token });
            else
                return NoContent();
        }
    }
}
