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
        public IActionResult GetIssues([FromQuery]IssueGetFilters Filters)
        {
            List<IssueGetResponse> Data = myDataRepository.GetIssues(Filters);

            return Ok(Data);
        }

        [HttpGet("{Id}")]
        public IActionResult GetIssue(int Id)
        {
            IssueGetResponse Issue = myDataRepository.GetIssue(Id);

            return Ok(Issue);
        }

        [HttpPut("{Id}")]
        public IActionResult UpdateIssue(int Id, [FromBody]IssuePostRequest Issue)
        {
            IssueGetResponse updatedIssue = myDataRepository.UpdateIssue(Id, Issue);

            if (updatedIssue != null)
                return Ok(updatedIssue);
            else
                return NotFound();
        }

        [HttpPost]
        public IActionResult CreateIssue(IssuePostRequest Issue)
        {
            IssueGetResponse CreatedIssue = myDataRepository.CreateIssue(Issue);

            return Ok(CreatedIssue);
        }

        [HttpDelete("{Id}")]
        public IActionResult DeleteIssue(int Id)
        {
            bool deleted = myDataRepository.DeleteIssue(Id);

            return Ok(deleted ? true : false);
        }

        [HttpGet("{IssueId}/Notes")]
        public IActionResult GetIssueNotes(int IssueId)
        {
            List<NoteGetResponse> response = myDataRepository.GetIssueNotes(IssueId);

            return Ok(response);
        }
    }
}
