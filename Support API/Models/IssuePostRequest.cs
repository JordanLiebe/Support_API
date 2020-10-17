using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Support_API.Models
{
    public class IssuePostRequest
    {
        [MaxLength(300)]
        public string Subject { get; set; }
        [MaxLength(25)]
        public string Priority { get; set; }
        [MaxLength(100)]
        public string Category { get; set; }
        [MaxLength(100)]
        public string Department { get; set; }
        [MaxLength(150)]
        public string Author { get; set; }
        public string Initial_Note { get; set; }
        [MaxLength(100)]
        public string Status { get; set; }
    }
}
