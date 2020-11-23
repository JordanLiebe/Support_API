using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Support_API.Models
{
    public class SingleEmailPost
    {
        public string From_Email { get; set; }
        public string From_Name { get; set; }
        public string To_Email { get; set; }
        public string To_Name { get; set; }
        public string Subject { get; set; }
        public string Content_Plain { get; set; }
        public string Content_Html { get; set; }
    }
}
