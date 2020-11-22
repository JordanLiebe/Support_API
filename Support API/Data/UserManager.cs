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
        private readonly ISessionManager _sessionManager;

        public UserManager(IConfiguration configuration, ISessionManager sessionManager)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("DefaultConnection");
            _sessionManager = sessionManager;
        }

        // Storage for Currently Logged In User //
        public User CurrentUser { get; set; }

        // User Management Functions //
        public CreateUserResponse CreateUser(string firstName, string middleName, string lastName, string login, string password)
        {
            // Generate UUID for user //
            Guid uuid = System.Guid.NewGuid();

            // Create hash for users password //
            string hash = Hashing.GenerateHash(password);

            using(var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                CreateUserResponse crResponse = new CreateUserResponse
                {
                    User = null,
                    Success = false
                };

                crResponse.User = connection
                    .Query<User>(
                        "EXEC [Support-API].[dbo].[SP_Create_User] @UUID = @UUID, @Login = @Login, @Hash = @Hash, @First_Name = @First_Name, @Middle_Name = @Middle_Name, @Last_Name = @Last_Name",
                        new { UUID = uuid.ToString(), Login = login, Hash = hash, First_Name = firstName, Middle_Name = middleName, Last_Name = lastName }
                    ).FirstOrDefault();

                if (crResponse.User != null)
                    crResponse.Success = true;

                return crResponse;
            }
        }
        public AuthUserResponse AuthenticateUser(string login, string password)
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

            AuthUserResponse loginResponse = new AuthUserResponse
            {
                Login = login,
                JWT = string.Empty,
                RequireMFA = true,
                Errors = new List<string>(),
                Success = false
            };

            if (user == null || user.Hash != hash)
            {
                loginResponse.Errors.Add("Invalid Username or Password");
            }
            else
            {
                var token = JWT.GenerateToken(user.UUID, user.Login);

                int code = NumberGen.Random(111111, 999999);
                string hashedCode = Hashing.GenerateHash(code.ToString());

                Session session = _sessionManager.CreateSession(user, token, hashedCode);

                if(token == null || session == null)
                {
                    loginResponse.Errors.Add("Authentication Error, please contact Administrator.");
                }
                else
                {
                    loginResponse.Success = true;
                    loginResponse.JWT = token;
                }
            }

            return loginResponse;
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
    }
}
