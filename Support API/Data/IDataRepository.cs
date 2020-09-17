using Support_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Support_API.Data
{
    public interface IDataRepository
    {
        public List<Issue> GetIssues();
    }
}
