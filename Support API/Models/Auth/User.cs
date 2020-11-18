using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace Support_API.Models.Auth
{
    public class User
    {
        public User() { }
        public User(User user, bool IncludeHash = true)
        {
            UUID = user.UUID;
            Login = user.Login;
            Hash = IncludeHash ? user.Hash : "******";
            First_Name = user.First_Name;
            Middle_Name = user.Middle_Name;
            Last_Name = user.Last_Name;
            Created = user.Created;
            Status = user.Status;
        }
        // Unique User Identifier  //
        public string UUID { get; set; }
        // User Credentials for verifying Identity //
        public string Login { get; set; }
        [IgnoreDataMember]
        public string Hash { get; set; }
        // General User Information //
        public string First_Name { get; set; }
        public string Middle_Name { get; set; }
        public string Last_Name { get; set; }
        // System Information //
        public DateTime Created { get; set; }
        public string Status { get; set; }
    }
}
