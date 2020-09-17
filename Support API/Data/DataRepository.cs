using Support_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Support_API.Data
{
    public class DataRepository : IDataRepository
    {
        public List<Issue> GetIssues()
        {
            List<Issue> Issues = new List<Issue>();
            Issues.Add(new Issue { Id = 1, Subject = "Hello World!", Author = "Jordan Liebe" });

            return Issues;
        }
    }
}
