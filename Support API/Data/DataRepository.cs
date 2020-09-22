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
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;
        public DataRepository(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public List<IssueGetResponse> GetIssuesAndNotes()
        {
            using(var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                var issueDictionary = new Dictionary<int, IssueGetResponse>();

                return connection
                    .Query<IssueGetResponse, NoteGetResponse, IssueGetResponse>(
                        "EXEC [dbo].[SP_Get_Issues_And_Notes]",
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
                        splitOn: "Id")
                    .Distinct()
                    .ToList();
            }
        }

        public List<NoteGetResponse> GetNotes(int IssueId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                var noteDictionary = new Dictionary<int, NoteGetResponse>();

                return connection
                    .Query<NoteGetResponse>(
                        "EXEC [dbo].[SP_Get_Notes] @IssueId = @IssueId", new { IssueId = IssueId })
                    .Distinct()
                    .ToList();
            }
        }
    }
}
