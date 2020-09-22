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
    public class NoteController : ControllerBase
    {
        public readonly IDataRepository myDataRepository;
        public NoteController(IDataRepository dataRepository)
        {
            myDataRepository = dataRepository;
        }

        [HttpGet("{IssueId}")]
        public IActionResult GetNotes(int IssueId)
        {
            List<NoteGetResponse> Notes = myDataRepository.GetNotes(IssueId);
            return Ok(Notes);
        }
    }
}
