﻿using Support_API.Models.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Support_API.Data
{
    public interface ISessionManager
    {
        public Session CreateSession(User user, string JWT);
        public Session GetLatestSession(string UUID);
    }
}
