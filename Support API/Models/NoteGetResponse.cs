using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Support_API.Models
{
    public class NoteGetResponse
    {
        public int Id { get; set; }
        public int IssueId { get; set; }
        public string Content { get; set; }
        public string Flag { get; set; }
        public string Author { get; set; }
        public DateTime Created { get; set; }
    }
}
