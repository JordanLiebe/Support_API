using Microsoft.AspNetCore.Authorization;
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

        [HttpGet]
        public IActionResult GetNotes()
        {
            List<NoteGetResponse> notes = myDataRepository.GetNotes();
            return Ok(notes);
        }

        [HttpGet("{Id}")]
        public IActionResult GetNotes(int Id)
        {
            NoteGetResponse note = myDataRepository.GetNote(Id);

            if (note != null)
                return Ok(note);
            else
                return NotFound();
        }

        [HttpPost]
        public IActionResult CreateNote(NotePostRequest note)
        {
            NoteGetResponse newNote = myDataRepository.CreateNote(note);

            return Ok(newNote);
        }

        [HttpPut("{Id}")]
        public IActionResult UpdateNote(int Id, [FromBody]NotePostRequest note)
        {
            NoteGetResponse updatedNote = myDataRepository.UpdateNote(Id, note);

            if (updatedNote != null)
                return Ok(updatedNote);
            else
                return NotFound();
        }

        [HttpDelete("{Id}")]
        public IActionResult DeleteNote(int Id)
        {
            bool success = myDataRepository.DeleteNote(Id);

            return Ok(new { Id = Id, Success = success });
        }
    }
}
