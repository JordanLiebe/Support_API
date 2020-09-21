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

        public List<Issue> GetIssues()
        {
            using(var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                var issueDictionary = new Dictionary<int, Issue>();

                return connection
                    .Query<Issue, Note, Issue>(
                        "EXEC [dbo].[SP_Get_Issues]",
                        map: (I, N) =>
                        {
                            Issue issue;

                            if (!issueDictionary.TryGetValue(I.Id, out issue))
                            {
                                issue = I;
                                issue.Notes =
                                    new List<Note>();
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
    }
}
