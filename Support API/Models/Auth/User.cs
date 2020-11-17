using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Support_API.Models.Auth
{
    public class User
    {
        // Unique User Identifier  //
        public string UUID { get; set; }
        // User Credentials for verifying Identity //
        public string Login { get; set; }
        [JsonIgnore]
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
