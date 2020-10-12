using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Support_API.Models
{
    public class IssueGetFilters
    {
        public int? Id { get; set; }
        public string Subject { get; set; }
        public string Priority { get; set; }
        public string Category { get; set; }
        public string Department { get; set; }
        public string Status { get; set; }
        public string Author { get; set; }
        public string Assignee { get; set; }
    }
}
