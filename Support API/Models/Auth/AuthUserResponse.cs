﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Support_API.Models.Auth
{
    public class AuthUserResponse
    {
        public string Login { get; set; }
        public bool Success { get; set; }
        public bool RequireMFA { get; set; }
        public List<string> Errors { get; set; }
        public string JWT { get; set; }
    }
}
