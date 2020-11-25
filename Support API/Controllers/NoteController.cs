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
        private readonly IDataRepository _dataRepository;
        private readonly IUserManager _userManager;
        public NoteController(IDataRepository dataRepository, IUserManager userManager)
        {
            _dataRepository = dataRepository;
            _userManager = userManager;
        }

        [HttpGet]
        public IActionResult GetNotes()
        {
            if (_userManager.CurrentUser == null)
                return Unauthorized();

            List<NoteGetResponse> notes = _dataRepository.GetNotes();
            return Ok(notes);
        }

        [HttpGet("{Id}")]
        public IActionResult GetNotes(int Id)
        {
            if (_userManager.CurrentUser == null)
                return Unauthorized();

            NoteGetResponse note = _dataRepository.GetNote(Id);

            if (note != null)
                return Ok(note);
            else
                return NotFound();
        }

        [HttpPost]
        public IActionResult CreateNote(NotePostRequest note)
        {
            if (_userManager.CurrentUser == null)
                return Unauthorized();

            NoteGetResponse newNote = _dataRepository.CreateNote(note);

            return Ok(newNote);
        }

        [HttpPut("{Id}")]
        public IActionResult UpdateNote(int Id, [FromBody]NotePostRequest note)
        {
            if (_userManager.CurrentUser == null)
                return Unauthorized();

            NoteGetResponse updatedNote = _dataRepository.UpdateNote(Id, note);

            if (updatedNote != null)
                return Ok(updatedNote);
            else
                return NotFound();
        }

        [HttpDelete("{Id}")]
        public IActionResult DeleteNote(int Id)
        {
            if (_userManager.CurrentUser == null)
                return Unauthorized();

            bool success = _dataRepository.DeleteNote(Id);

            return Ok(new { Id = Id, Success = success });
        }
    }
}
