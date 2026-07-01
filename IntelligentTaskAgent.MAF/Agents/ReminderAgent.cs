using IntelligentTaskAgent.MAF.Enum;
using IntelligentTaskAgent.MAF.Instructions;
using IntelligentTaskAgent.MAF.Instructions.Reminder;
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

        private readonly ChatClientAgent agent;
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

                    AIFunctionFactory.Create(reminderPlugin.DeleteReminderAsync),
                    
                    AIFunctionFactory.Create(reminderPlugin.SearchReminderAsync)
                };

            var prompt = PromptBuilder
                .Create("You are an Enterprise Reminder Assistant.")
                .WithCommon()
                .With(ReminderInstructions.Prompt)
                .Build();

            agent = new ChatClientAgent(
                chatClient,
                instructions: prompt,
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

            var context =
            await conversationMemory.GetAsync(
                conversationId);

            if (context == null)
            {
                var session =
                    await agent.CreateSessionAsync(
                        cancellationToken);

                context = new ConversationContext
                {
                    ConversationId = conversationId,
                    Session = session
                };
            }

            AgentResponse response =
                await agent.RunAsync(
                    request.Message,
                    context.Session,
                    null,
                    cancellationToken);

            await conversationMemory.SaveAsync(context);

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
