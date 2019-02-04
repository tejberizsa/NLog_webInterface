using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using LogiAppMonitor.API.Data;
using LogiAppMonitor.API.Dtos;
using LogiAppMonitor.API.Helpers;
using LogiAppMonitor.API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace LogiAppMonitor.API.Controllers
{
    [ServiceFilter(typeof(LogUserActivity))]
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class LoggerController : ControllerBase
    {
        private readonly IApplicationRepository _repo;
        private readonly IMapper _mapper;
        public LoggerController(IApplicationRepository repo, IMapper mapper)
        {
            _mapper = mapper;
            _repo = repo;

        }

        [HttpGet]
        public async Task<IActionResult> GetLogs([FromQuery]PageParams pageParams)
        {
            var logs = await _repo.GetLogMessages(pageParams);
            Response.AddPagination(logs.CurrentPage, logs.PageSize, logs.TotalCount, logs.TotalPages);
            return Ok(logs);
        }

        [HttpGet("{id}", Name = "GetLog")]
        public async Task<IActionResult> GetLog(int id)
        {
            var log = await _repo.GetLogMessage(id);
            return Ok(log);
        }

        [HttpPost("AddLogs")]
        public async Task<IActionResult> AddLogs([FromBody] string jsonContent)
        {
            var logData = System.IO.File.ReadAllText("Data/UserSeedData.json");
            var settings = new JsonSerializerSettings
                    {
                        NullValueHandling = NullValueHandling.Ignore,
                        MissingMemberHandling = MissingMemberHandling.Ignore
                    };
            var logs = JsonConvert.DeserializeObject<List<LogMessage>>(logData, settings);
            foreach (var log in logs)
            {
                _repo.Add(log);
            }
            if(await _repo.SaveAll()) {
                return Ok();
            }
            return BadRequest("Save json data failed.");
        }
    }
}