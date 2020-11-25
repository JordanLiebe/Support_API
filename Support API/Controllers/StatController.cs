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
    public class StatController : ControllerBase
    {
        public readonly IDataRepository _dataRepository;
        private readonly IUserManager _userManager;
        public StatController(IDataRepository dataRepository, IUserManager userManager)
        {
            _dataRepository = dataRepository;
            _userManager = userManager;
        }

        [HttpGet("Global")]
        public IActionResult GetGlobalStatus()
        {
            List<GlobalStat> stats = _dataRepository.GetGlobalStats();

            return Ok(stats);
        }
    }
}
