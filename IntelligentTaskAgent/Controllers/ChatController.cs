using IntelligentTaskAgent.MAF.Runtime;
using Microsoft.Agents.AI;
using Microsoft.AspNetCore.Mvc;
using IntelligentTaskAgent.MAF.Models.Responses;
using IntelligentTaskAgent.MAF.Models.Requests;
namespace IntelligentTaskAgent.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ChatController : Controller
    {
        private readonly IAgentRuntime _agentHost;

        public ChatController(IAgentRuntime agentHost)
        {
            _agentHost = agentHost;
        }

        [HttpPost]
        [HttpPost]
        public async Task<ActionResult<ConversationResponse>> Chat(
    [FromBody] ConversationRequest request,
    CancellationToken cancellationToken)
        {
            try
            {
                var response = await _agentHost
                    .ReminderAgent
                    .ChatAsync(request,
                        cancellationToken);

                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }
    }
}
