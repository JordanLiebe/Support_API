using Support_API.Models.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Threading.Tasks;

namespace Support_API.Data
{
    public interface IUserManager
    {
        // Storage for Currently Logged In User //
        public User CurrentUser { get; set; }

        // User Management Functions //
        public CreateUserResponse CreateUser(string firstName, string middleName, string lastName, string login, string password);
        public Task<AuthUserResponse> AuthenticateUser(string login, string password);
        public User GetUser(string uuid);
    }
}
