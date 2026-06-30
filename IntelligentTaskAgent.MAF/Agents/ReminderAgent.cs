using IntelligentTaskAgent.MAF.Enum;
using IntelligentTaskAgent.MAF.Memory;
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
        public AgentType AgentType => AgentType.Reminder;

        private readonly ChatClientAgent _agent;
        private readonly IConversationMemory conversationMemory;
        public ReminderAgent(
            IChatClient chatClient,
            IServiceProvider serviceProvider,
            IConversationMemory conversationMemory)
        {

            this.conversationMemory = conversationMemory;

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
            var conversationId =
    string.IsNullOrWhiteSpace(request.ConversationId)
        ? Guid.NewGuid().ToString()
        : request.ConversationId;

            var session =
                await conversationMemory.GetAsync(conversationId);

            if (session == null)
            {
                session = await _agent.CreateSessionAsync(
                    cancellationToken);
            }

            AgentResponse response =
                await _agent.RunAsync(
                    request.Message,
                    session,
                    null,
                    cancellationToken);
            await conversationMemory.SaveAsync(
                conversationId,
                session);

            return new ConversationResponse
            {
                Message = response.Text,
                ConversationId = conversationId,
                AgentId = response.AgentId,
                CreatedAt = response.CreatedAt
            };
        }
    }
}
