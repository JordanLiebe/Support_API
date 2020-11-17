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
        public User CurrentUser { get; set; }
        // User Management Functions //
        public User CreateUser(string firstName, string middleName, string lastName, string login, string password);
        public string AuthenticateUser(string login, string password);
        public User GetUser(string uuid);

        // Session Management Functions //
        public Session CreateSession(User user, string JWT);
        public Session GetLatestSession(string UUID);
    }
}
