using IntelligentTaskAgent.MAF.Models.Requests;
using IntelligentTaskAgent.MAF.Models.Responses;
using Microsoft.Agents.AI;
using Microsoft.Extensions.AI;

namespace IntelligentTaskAgent.MAF.Agents
{

    public sealed class ReminderAgent : IReminderAgent
    {
        private readonly ChatClientAgent _agent;

        public ReminderAgent(
            IChatClient chatClient,
            IServiceProvider serviceProvider)
        {
            _agent = new ChatClientAgent(
                chatClient,
                instructions: """
You are an intelligent Reminder Assistant.

Responsibilities:
- Create reminders
- Update reminders
- Delete reminders
- Search reminders
- Update user profile

Rules:
- Be concise.
- Ask follow-up questions if information is missing.
- Never invent reminder details.
- Use available tools whenever possible.
""",
                name: "ReminderAgent",
                description: "Enterprise Reminder Assistant",
                services: serviceProvider);
        }

        public async Task<ConversationResponse> ChatAsync(
            ConversationRequest request,
            CancellationToken cancellationToken = default)
        {
            AgentSession? session = null;

            if (!string.IsNullOrWhiteSpace(request.ConversationId))
            {
                session = await _agent.CreateSessionAsync(
                    request.ConversationId,
                    cancellationToken);
            }

            AgentResponse response = await _agent.RunAsync(
                request.Message,
                session,
                null,
                cancellationToken);

            return new ConversationResponse
            {
                Message = response.Text,
                AgentId = response.AgentId,
                ConversationId = request.ConversationId,
                CreatedAt = response.CreatedAt
            };
        }
    }
}
