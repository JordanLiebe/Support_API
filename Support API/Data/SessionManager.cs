using Dapper;
using Microsoft.Extensions.Configuration;
using Support_API.Models.Auth;
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

        public Session CreateSession(User user, string JWT)
        {
            Session session = null;

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                session = connection
                    .Query<Session>(
                        "EXEC [Support-API].[dbo].[SP_Create_Session] @UUID = @UUID, @JWT = @JWT",
                        new { UUID = user.UUID, JWT = JWT }
                    ).FirstOrDefault();
            }

            return session;
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
