using IntelligentTaskAgent.Core.Services;
using Microsoft.AspNetCore.Mvc;

namespace IntelligentTaskAgent.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TaskController : Controller
    {
        private readonly TaskOrchestrationService taskOrchestrationService;
        public TaskController(
            TaskOrchestrationService taskOrchestrationService)
        {
            this.taskOrchestrationService = taskOrchestrationService;
        }
        [HttpPost("task")]
        public async Task<IActionResult> CreateTask([FromBody] string input)
        {
            await this.taskOrchestrationService.HandleAsync(input);
            return Ok("Task created");
        }
        [HttpGet("task")]
        public async Task<IActionResult> Get()
        {
            return Ok("Connected");

        }
    }
}
