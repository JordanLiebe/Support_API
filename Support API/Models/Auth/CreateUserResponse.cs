using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Support_API.Models.Auth
{
    public class CreateUserResponse
    {
        public bool Success { get; set; }
        public User User { get; set; }
    }
}
