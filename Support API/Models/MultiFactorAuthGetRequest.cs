using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Support_API.Models
{
    public class MultiFactorAuthGetRequest
    {
        public string JWT { get; set; }
        public string AuthCode { get; set; }
    }
}
