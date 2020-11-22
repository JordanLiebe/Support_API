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
        private readonly IUserManager _userManager;
        private readonly ISessionManager _sessionManager;
        public AuthController(IUserManager userManager, ISessionManager sessionManager)
        {
            _userManager = userManager;
            _sessionManager = sessionManager;
        }

        [HttpGet("Me")]
        public IActionResult GetMe()
        {
            User user = new User(_userManager.CurrentUser, false);

            return Ok(user);
        }

        [HttpPost("Create")]
        public IActionResult CreateUser(string First_Name, string Middle_Name, string Last_Name, string Login, string Password)
        {
            CreateUserResponse crResponse = _userManager.CreateUser(First_Name, Middle_Name, Last_Name, Login, Password);

            return Ok(crResponse);
        }

        [HttpPost("Login")]
        public IActionResult LoginUser([FromBody]LoginPost login)
        {
            AuthUserResponse authResponse = _userManager.AuthenticateUser(login.Username, login.Password);

            return Ok(authResponse);
        }

        [HttpPost("Code")]
        public IActionResult VerifyCode(int Code, string Token)
        {
            Session session = _sessionManager.VerifySession(Token, Code);

            return Ok();
        }
    }
}
