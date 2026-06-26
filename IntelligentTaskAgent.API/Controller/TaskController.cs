using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;


namespace IntelligentTaskAgent.API.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        [HttpPost("task")]
        public async Task<IActionResult> CreateTask([FromBody] string input)
        {
            //await _taskOrchestrationService.HandleAsync(input);
            return Ok("Task created");
        }
    }
}
