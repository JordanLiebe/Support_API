using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Support_API.Models
{
    public class NotePostRequest
    {
        public int IssueId { get; set; }
        public string Content { get; set; }
        public string Flag { get; set; }
        public string Author { get; set; }
    }
}
