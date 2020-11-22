using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Support_API.Models
{
    public class EmailRecord
    {
        public int Id { get; set; }
        public string FROM { get; set; }
        public List<string> TO { get; set; }
        public List<string> CC { get; set; }
        public List<string> BCC { get; set; }
        public string SUBJECT { get; set; }
        public string CONTENT { get; set; }
    }
}
