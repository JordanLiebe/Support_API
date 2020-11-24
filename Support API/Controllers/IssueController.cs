﻿using Microsoft.AspNetCore.Authorization;
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
        public readonly IDataRepository _dataRepository;
        private readonly IUserManager _userManager;
        public IssueController(IDataRepository dataRepository, IUserManager userManager)
        {
            _dataRepository = dataRepository;
            _userManager = userManager;
        }

        [HttpGet]
        public IActionResult GetIssues([FromQuery]IssueGetFilters Filters)
        {
            Filters.Author = _userManager.CurrentUser.UUID;
            List<IssueGetResponse> Data = _dataRepository.GetIssues(Filters);

            return Ok(Data);
        }

        [HttpGet("{Id}")]
        public IActionResult GetIssue(int Id)
        {
            IssueGetResponse Issue = _dataRepository.GetIssue(Id);

            return Ok(Issue);
        }

        [HttpPut("{Id}")]
        public IActionResult UpdateIssue(int Id, [FromBody]IssuePostRequest Issue)
        {
            IssueGetResponse updatedIssue = _dataRepository.UpdateIssue(Id, Issue);

            if (updatedIssue != null)
                return Ok(updatedIssue);
            else
                return NotFound();
        }

        [HttpPost]
        public IActionResult CreateIssue(IssuePostRequest Issue)
        {
            IssueGetResponse CreatedIssue = _dataRepository.CreateIssue(Issue);

            return Ok(CreatedIssue);
        }

        [HttpDelete("{Id}")]
        public IActionResult DeleteIssue(int Id)
        {
            bool deleted = _dataRepository.DeleteIssue(Id);

            return Ok(deleted ? true : false);
        }

        [HttpGet("{IssueId}/Notes")]
        public IActionResult GetIssueNotes(int IssueId)
        {
            List<NoteGetResponse> response = _dataRepository.GetIssueNotes(IssueId);

            return Ok(response);
        }
    }
}
