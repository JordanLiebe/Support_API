using Dapper;
using Microsoft.Extensions.Configuration;
using Support_API.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Support_API.Data
{
    public class DataRepository : IDataRepository
    {
        // Globals and Constructors //
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;
        private readonly IUserManager _userManager;
        public DataRepository(IConfiguration configuration, IUserManager userManager)
        {
            _configuration = configuration;
            _connectionString = configuration.GetConnectionString("DefaultConnection");
            _userManager = userManager;
        }

        // Issue Related Functions //
        public List<IssueGetResponse> GetIssues(IssueGetFilters Filters)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                var issueDictionary = new Dictionary<int, IssueGetResponse>();

                return connection
                .Query<IssueGetResponse, NoteGetResponse, IssueGetResponse>(
                    "EXEC [dbo].[SP_Get_Issues_Filtered] @Id = @Id, @Subject = @Subject, @Priority = @Priority, @Category = @Category, @Department = @Department, @Status = @Status, @Author = @Author, @Assignee = @Assignee",
                    map: (I, N) =>
                    {
                        IssueGetResponse issue;

                        if (!issueDictionary.TryGetValue(I.Id, out issue))
                        {
                            issue = I;
                            issue.Notes =
                                new List<NoteGetResponse>();
                            issueDictionary.Add(issue.Id, issue);
                        }

                        issue.Notes.Add(N);
                        return issue;
                    },
                    param: new { Id = Filters.Id, Subject = Filters.Subject, Priority = Filters.Priority, Category = Filters.Category, Department = Filters.Department, Status = Filters.Status, Author = Filters.Author, Assignee = Filters.Assignee },
                    splitOn: "Id")
                .Distinct()
                .ToList();
            }
        }
        public IssueGetResponse CreateIssue(IssuePostRequest Issue)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                return connection
                    .Query<IssueGetResponse>(
                        "EXEC [Support-API].[dbo].[SP_Create_Issue] @Subject = @Subject, @Priority = @Priority, @Category = @Category, @Department = @Department, @Initial_Note = @Initial_Note, @Author = @Author, @Status = @Status",
                        new { Subject = Issue.Subject, Priority = Issue.Priority, Category = Issue.Category, Department = Issue.Department, Initial_Note = Issue.Initial_Note, Author = _userManager.CurrentUser.UUID, Status = "NEW" }
                    ).FirstOrDefault();
            }
        }
        public IssueGetResponse GetIssue(int Id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                var issueDictionary = new Dictionary<int, IssueGetResponse>();

                return connection
                .Query<IssueGetResponse, NoteGetResponse, IssueGetResponse>(
                    "EXEC [dbo].[SP_Get_Issue] @Id = @Id",
                    map: (I, N) =>
                    {
                        IssueGetResponse issue;

                        if (!issueDictionary.TryGetValue(I.Id, out issue))
                        {
                            issue = I;
                            issue.Notes =
                                new List<NoteGetResponse>();
                            issueDictionary.Add(issue.Id, issue);
                        }

                        issue.Notes.Add(N);
                        return issue;
                    },
                    param: new { Id = Id },
                    splitOn: "Id")
                .Distinct()
                .FirstOrDefault();
            }
        }
        public IssueGetResponse UpdateIssue(int Id, IssuePostRequest Issue)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                return connection
                    .Query<IssueGetResponse>(
                        "EXEC [Support-API].[dbo].[SP_Update_Issue] @Id = @Id, @Subject = @Subject, @Priority = @Priority, @Category = @Category, @Department = @Department, @Author = @Author, @Status = @Status",
                        new { Id = Id, Subject = Issue.Subject, Priority = Issue.Priority, Category = Issue.Category, Department = Issue.Department, Author = Issue.Author, Status = Issue.Status }
                    ).FirstOrDefault();
            }
        }
        public bool DeleteIssue(int Id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                var noteDictionary = new Dictionary<int, NoteGetResponse>();

                var deleted = connection
                    .Query<DeleteRecord>(
                        "EXEC [dbo].[SP_Delete_Issue] @Id = @Id", new { Id = Id })
                    .FirstOrDefault();

                return deleted.Success == 1 ? true : false;
            }
        }
        public List<NoteGetResponse> GetIssueNotes(int IssueId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                return connection
                    .Query<NoteGetResponse>(
                        "EXEC [dbo].[SP_Get_Issue_Notes] @IssueId = @IssueId", new { IssueId = IssueId })
                    .Distinct()
                    .ToList();
            }
        }

        // Note Related Functions //
        public List<NoteGetResponse> GetNotes()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                return connection
                    .Query<NoteGetResponse>(
                        "EXEC [dbo].[SP_Get_Notes]")
                    .Distinct()
                    .ToList();
            }
        }
        public NoteGetResponse CreateNote(NotePostRequest Note)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                 
                return connection
                    .Query<NoteGetResponse>(
                        "EXEC [Support-API].[dbo].[SP_Create_Note] @IssueId = @IssueId, @Content = @Content, @Flag = @Flag, @Author = @Author",
                        new { IssueId = Note.IssueId, Content = Note.Content, Flag = Note.Flag, Author = Note.Author }
                    ).FirstOrDefault();
            }
        }
        public NoteGetResponse GetNote(int Id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                return connection
                    .Query<NoteGetResponse>(
                        "EXEC [dbo].[SP_Get_Note] @Id = @Id", new { Id = Id })
                    .Distinct()
                    .FirstOrDefault();
            }
        }
        public NoteGetResponse UpdateNote(int Id, NotePostRequest Note)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                return connection
                    .Query<NoteGetResponse>(
                        "EXEC [dbo].[SP_Update_Note] @Id = @Id, @IssueId = @IssueId, @Content = @Content, @Flag = @Flag, @Author = @Author", new { Id = Id, IssueId = Note.IssueId, Content = Note.Content, Flag = Note.Flag, Author = Note.Author })
                    .Distinct()
                    .FirstOrDefault();
            }
        }
        public bool DeleteNote(int Id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                var noteDictionary = new Dictionary<int, NoteGetResponse>();

                var deleted = connection
                    .Query<DeleteRecord>(
                        "EXEC [dbo].[SP_Delete_Note] @Id = @Id", new { Id = Id })
                    .FirstOrDefault();

                return deleted.Success == 1 ? true : false;
            }
        }

        // Stat Related Functions //
        public List<GlobalStat> GetGlobalStats()
        {
            List<GlobalStat> stats = null;

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                stats = connection.Query<GlobalStat>("EXEC [dbo].[SP_Stats_Global]").ToList();
            }

            return stats;
        }
    }
}
