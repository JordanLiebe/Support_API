using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Support_API.Data;
using Support_API.Models.Auth;
using Support_API.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Support_API.Middleware
{
    public static class AuthenticationMiddlewareExtension
    {
        public static IApplicationBuilder UseAuthenticationMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<AuthenticationMiddleware>();
        }
    }

    public class AuthenticationMiddleware
    {
        private readonly RequestDelegate _next;

        public AuthenticationMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context, IConfiguration _configuration, IUserManager _userManager, ISessionManager _sessionManager)
        {
            IHeaderDictionary headers = context.Request.Headers;
            string[] Unlocked = { "/Auth/Login", "/Auth/Code", "/Auth/Register" };

            string authHeader = headers["Authorization"];
            string path = context.Request.Path;

            if (Unlocked.Where(e => e == path).FirstOrDefault() == null)
            {
                if (authHeader != null && authHeader != string.Empty)
                {
                    string[] tokenBreak = authHeader.Split(" ");

                    if (tokenBreak.Length == 2)
                    {
                        string AuthToken = tokenBreak[1];
                        string JwtSecret = _configuration.GetValue<string>("JwtSecret");
                        string UUID = JWT.ValidateJwtToken(AuthToken, JwtSecret);

                        if(UUID != null)
                        {
                            User user = _userManager.GetUser(UUID);

                            if(user != null)
                            {
                                Session latestSession = _sessionManager.GetLatestSession(UUID);

                                if(latestSession.JWT == AuthToken && latestSession.UUID == UUID)
                                {
                                    _userManager.CurrentUser = user;
                                    await _next(context);
                                    return;
                                }
                            }
                        }
                    }
                }

                context.Response.StatusCode = 401;
                return;
            }

            await _next(context);
        }
    }
}
