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
        public IActionResult CreateUser(CreateUserRequest Request)
        {
            CreateUserResponse crResponse = _userManager.CreateUser(Request.Login, Request.Password, Request.Email, Request.First_Name, Request.Middle_Name, Request.Last_Name);

            return Ok(crResponse);
        }

        [HttpPost("Login")]
        public async Task<IActionResult> LoginUser([FromBody]LoginPost login)
        {
            AuthUserResponse authResponse = await _userManager.AuthenticateUser(login.Username, login.Password);

            return Ok(authResponse);
        }

        [HttpPost("Code")]
        public IActionResult VerifyCode([FromBody]CodePost post)
        {
            AuthUserResponse authResponse = _sessionManager.VerifySession(post.Token, post.Code);

            return Ok(authResponse);
        }
    }
}
