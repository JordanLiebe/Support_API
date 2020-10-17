using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Support_API.Data;
using Support_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Support_API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class IssueController : ControllerBase
    {
        public readonly IDataRepository myDataRepository;
        public IssueController(IDataRepository dataRepository)
        {
            myDataRepository = dataRepository;
        }

        [HttpGet]
        public IActionResult GetIssues([FromQuery]IssueGetFilters? Filters)
        {
            List<IssueGetResponse> Data = myDataRepository.GetIssuesAndNotes(Filters);

            return Ok(Data);
        }

        [HttpPost]
        public IActionResult CreateIssue(IssuePostRequest Issue)
        {
            IssueGetResponse CreatedIssue = myDataRepository.CreateIssue(Issue);

            return Ok(CreatedIssue);
        }
    }
}
