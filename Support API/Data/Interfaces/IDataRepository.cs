using Support_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Support_API.Data
{
    public interface IDataRepository
    {
        // Issue Related Functions //
        public List<IssueGetResponse> GetIssues(IssueGetFilters Filters);//SP_Get_Issues_And_Notes_Filter
        public IssueGetResponse CreateIssue(IssuePostRequest Issue);//SP_Create_Issue
        public IssueGetResponse GetIssue(int Id);//SP_Get_Issue
        public IssueGetResponse UpdateIssue(int Id, IssuePostRequest Issue);//SP_Update_Issue
        public bool DeleteIssue(int Id);//SP_Delete_Issue
        public List<NoteGetResponse> GetIssueNotes(int IssueId);

        // Note Related Functions //
        public List<NoteGetResponse> GetNotes();//SP_Get_Notes
        public NoteGetResponse CreateNote(NotePostRequest Note);//SP
        public NoteGetResponse GetNote(int Id);//
        public NoteGetResponse UpdateNote(int Id, NotePostRequest Note);//
        public bool DeleteNote(int Id);//
    }
}
