using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Support_API.Models.Auth
{
    public class CodeResponse
    {
        public bool Success { get; set; }
        public List<string> Errors { get; set; }
    }
}
