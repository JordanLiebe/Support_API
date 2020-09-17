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
        public IActionResult Index()
        {
            List<Issue> Data = myDataRepository.GetIssues();

            return Ok(Data);
        }
    }
}
