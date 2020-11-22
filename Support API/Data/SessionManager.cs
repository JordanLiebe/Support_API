using Dapper;
using Microsoft.Extensions.Configuration;
using Support_API.Models.Auth;
using Support_API.Tools;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Support_API.Data
{
    public class SessionManager : ISessionManager
    {
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;

        public SessionManager(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("DefaultConnection");
        }

        public Session CreateSession(User user, string JWT, string Code)
        {
            Session session = null;

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                session = connection
                    .Query<Session>(
                        "EXEC [Support-API].[dbo].[SP_Create_Session] @UUID = @UUID, @JWT = @JWT, @CODE = @CODE",
                        new { UUID = user.UUID, JWT = JWT, CODE = Code }
                    ).FirstOrDefault();
            }

            return session;
        }

        public AuthUserResponse VerifySession(string Token, int Code)
        {
            Session session = null;
            AuthUserResponse authResponse = new AuthUserResponse
            {
                JWT = Token,
                RequireMFA = true,
                Errors = new List<string>(),
                Success = false
            };

            string CodeStr = Code.ToString();
            string HashedCodeStr = Hashing.GenerateHash(CodeStr);

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                session = connection
                    .Query<Session>(
                        "EXEC [Support-API].[dbo].[SP_Verify_Session] @JWT = @JWT, @CODE = @CODE",
                        new { JWT = Token, CODE = HashedCodeStr }
                    ).FirstOrDefault();
            }

            if(session.Verified)
            {
                authResponse.Success = true;
                authResponse.RequireMFA = false;
            }
            else
            {
                authResponse.Errors.Add("Invalid Code or Token");
            }

            return authResponse;
        }

        public Session GetLatestSession(string UUID)
        {
            Session session = null;

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                session = connection
                    .Query<Session>(
                        "EXEC [Support-API].[dbo].[SP_Get_Latest_Session] @UUID = @UUID",
                        new { UUID = UUID }
                    ).FirstOrDefault();
            }

            return session;
        }
    }
}
