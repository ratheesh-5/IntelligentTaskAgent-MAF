using IntelligentTaskAgent.MAF.Models.Requests;
using IntelligentTaskAgent.MAF.Models.Responses;
using IntelligentTaskAgent.MAF.Plugins;
using Microsoft.Agents.AI;
using Microsoft.Extensions.AI;
using Microsoft.Extensions.DependencyInjection;

namespace IntelligentTaskAgent.MAF.Agents
{

    public sealed class ReminderAgent : IReminderAgent
    {
        private readonly ChatClientAgent _agent;

        public ReminderAgent(
            IChatClient chatClient,
            IServiceProvider serviceProvider)
        {


            // Resolve plugin from DI
            var reminderPlugin =
                serviceProvider.GetRequiredService<IReminderPlugin>();

            var tools = new List<AITool>
                {
                    AIFunctionFactory.Create(reminderPlugin.CreateReminderAsync),

                    AIFunctionFactory.Create(reminderPlugin.UpdateReminderAsync),

                    AIFunctionFactory.Create(reminderPlugin.DeleteReminderAsync)
                };

            _agent = new ChatClientAgent(
                chatClient,
               instructions: ReminderInstructions.SystemPrompt,
                name: "ReminderAgent",
                description: "Enterprise Reminder Assistant",
                tools: tools,
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
