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

        public CodeResponse VerifySession(string Token, int Code)
        {
            Session session = null;
            CodeResponse response = new CodeResponse
            {
                Success = false,
                Errors = new List<string>(),
            };

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                session = connection
                    .Query<Session>(
                        "EXEC [Support-API].[dbo].[SP_Get_Session] @JWT = @JWT",
                        new { JWT = Token }
                    ).FirstOrDefault();
            }

            Hash codeHash = new Hash(session.Code);
            string CodeStr = Code.ToString();
            string HashedCodeStr = Hashing.GenerateHash(CodeStr, codeHash.iterations, codeHash.salt);

            if (session.Code == HashedCodeStr)
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();

                    session = connection
                        .Query<Session>(
                            "EXEC [Support-API].[dbo].[SP_Verify_Session] @JWT = @JWT",
                            new { JWT = Token }
                        ).FirstOrDefault();
                }

                if (session.Verified)
                {
                    response.Success = true;
                }
                else
                {
                    response.Errors.Add("Invalid Code");
                }
            }
            else
            {
                response.Errors.Add("Invalid Token");
            }

            return response;
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
