using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Support_API.Models.Auth
{
    public class Session
    {
        public int Id { get; set; }
        public string UUID { get; set; }
        public string JWT { get; set; }
        public string Code { get; set; }
        public bool Verified { get; set; }
        public DateTime Created { get; set; }
    }
}
