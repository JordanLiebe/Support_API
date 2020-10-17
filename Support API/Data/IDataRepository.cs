using Support_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Support_API.Data
{
    public interface IDataRepository
    {
        public List<IssueGetResponse> GetIssuesAndNotes(IssueGetFilters? Filters);
        public List<NoteGetResponse> GetNotes(int IssueId);
        public IssueGetResponse CreateIssue(IssuePostRequest Issue);
    }
}
