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
        public IActionResult GetNotes()
        {
            List<NoteGetResponse> Notes = myDataRepository.GetNotes();
            return Ok(Notes);
        }

        [HttpPost]
        public IActionResult CreateNote(NotePostRequest note)
        {
            NoteGetResponse response = myDataRepository.CreateNote(note);

            return Ok(Response);
        }

        [HttpPut("{Id}")]
        public IActionResult UpdateNote(int Id, [FromBody]NotePostRequest note)
        {


            return NotFound();
        }

        [HttpDelete("{Id}")]
        public IActionResult DeleteNote(int Id)
        {
            bool Success = myDataRepository.DeleteNote(Id);

            return Ok(Success);
        }
    }
}
