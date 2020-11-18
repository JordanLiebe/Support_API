using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Support_API.Models.Auth
{
    public class LoginPost
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
