using Dapper;
using Microsoft.Extensions.Configuration;
using Support_API.Models.Auth;
using Support_API.Tools;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Security;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Support_API.Data
{
    public class UserManager : IUserManager
    {
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;

        public UserManager(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("DefaultConnection");
        }

        public User CurrentUser { get; set; }

        // User Management Functions //
        public User CreateUser(string firstName, string middleName, string lastName, string login, string password)
        {
            // Generate UUID for user //
            Guid uuid = System.Guid.NewGuid();

            // Create hash for users password //
            string hash = Hashing.GenerateHash(password);

            using(var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                return connection
                    .Query<User>(
                        "EXEC [Support-API].[dbo].[SP_Create_User] @UUID = @UUID, @Login = @Login, @Hash = @Hash, @First_Name = @First_Name, @Middle_Name = @Middle_Name, @Last_Name = @Last_Name",
                        new { UUID = uuid.ToString(), Login = login, Hash = hash, First_Name = firstName, Middle_Name = middleName, Last_Name = lastName }
                    ).FirstOrDefault();
            }
        }
        public string AuthenticateUser(string login, string password)
        {
            string hash = Hashing.GenerateHash(password);
            User user = null;

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                user = connection
                    .Query<User>(
                        "EXEC [Support-API].[dbo].[SP_Get_User] @Login = @Login",
                        new { Login = login }
                    ).FirstOrDefault();
            }

            if (user!= null)
            {
                if(user.Hash == hash)
                {
                    var token = JWT.GenerateToken(user.UUID, user.Login);
                    var uuid = JWT.ValidateJwtToken(token);

                    CreateSession(user, token);

                    return token;
                }
                else
                {
                    return null;
                }    
            }
            else
                return null;
        }

        public User GetUser(string uuid)
        {
            User user = null;

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                user = connection
                    .Query<User>(
                        "EXEC [Support-API].[dbo].[SP_Get_User_By_UUID] @UUID = @UUID",
                        new { UUID = uuid }
                    ).FirstOrDefault();
            }

            return user;
        }

        // Session Management Functions //
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
