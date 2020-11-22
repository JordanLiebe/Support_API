using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Support_API.Models.Auth
{
    public class CodePost
    {
        public int Code { get; set; }
        public string Token { get; set; }
    }
}
