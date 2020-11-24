using Dapper;
using Microsoft.Extensions.Configuration;
using Support_API.Models;
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
        public CreateUserResponse CreateUser(string login, string password, string email, string firstName, string middleName, string lastName)
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
                        "EXEC [Support-API].[dbo].[SP_Create_User] @UUID = @UUID, @Login = @Login, @Hash = @Hash, @Email = @Email, @First_Name = @First_Name, @Middle_Name = @Middle_Name, @Last_Name = @Last_Name",
                        new { UUID = uuid.ToString(), Login = login, Hash = hash, Email = email, First_Name = firstName, Middle_Name = middleName, Last_Name = lastName }
                    ).FirstOrDefault();

                if (crResponse.User != null)
                    crResponse.Success = true;

                return crResponse;
            }
        }
        public async Task<LoginResponse> AuthenticateUser(string login, string password)
        {
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

            LoginResponse response = new LoginResponse
            {
                Success = false,
                Errors = new List<string>(),
                JWT = string.Empty,
            };

            if (user == null)
            {
                response.Errors.Add("Invalid Username or Password");
            }
            else
            {
                Hash currentHash = new Hash(user.Hash);
                string hash = Hashing.GenerateHash(password, currentHash.iterations, currentHash.salt);

                if(hash == user.Hash)
                {
                    string JwtSecret = _configuration.GetValue<string>("JwtSecret");
                    var token = JWT.GenerateToken(user.UUID, user.Login, JwtSecret);

                    int code = Generator.RandomNum(111111, 999999);
                    string hashedCode = Hashing.GenerateHash(code.ToString());

                    string emailPlainTemplate = "Hello {0} {1} {2}! Your Verification Code is: {3}";
                    string emailHtmlTemplate = "<html>" +
                        "<body>" +
                        "<div>" +
                        "<h2>Support App</h3>" +
                        "<div>Hello {0} {1} {2}!</div>" +
                        "<div>Your Verification Code is: {3}</div>" +
                        "</div>" +
                        "</body>" +
                        "</html>";

                    string emailApiKey = _configuration.GetValue<string>("MailApiKey");
                    SingleEmailPost email = new SingleEmailPost
                    {
                        From_Email = "webmaster@jmliebe.com",
                        From_Name = "Support App",
                        To_Email = user.Email,
                        To_Name = $"{user.First_Name} {user.Middle_Name} {user.Last_Name}",
                        Subject = "Verification Email",
                        Content_Html = string.Format(emailHtmlTemplate, user.First_Name, user.Middle_Name, user.Last_Name, code),
                        Content_Plain = string.Format(emailPlainTemplate, user.First_Name, user.Middle_Name, user.Last_Name, code),
                    };
                    await Email.SingleEmail(email, emailApiKey);

                    Session session = _sessionManager.CreateSession(user, token, hashedCode);

                    if (token == null || session == null)
                    {
                        response.Errors.Add("Authentication Error, please contact Administrator.");
                    }
                    else
                    {
                        response.Success = true;
                        response.JWT = token;
                    }
                }
                else
                {
                    response.Errors.Add("Invalid Username or Password");
                }
            }

            return response;
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
