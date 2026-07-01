using IntelligentTaskAgent.MAF.Enum;
using IntelligentTaskAgent.MAF.Instructions;
using IntelligentTaskAgent.MAF.Instructions.UserProfile;
using IntelligentTaskAgent.MAF.Memory;
using IntelligentTaskAgent.MAF.Models;
using IntelligentTaskAgent.MAF.Models.Requests;
using IntelligentTaskAgent.MAF.Models.Responses;
using IntelligentTaskAgent.MAF.Plugins;
using Microsoft.Agents.AI;
using Microsoft.Extensions.AI;
using Microsoft.Extensions.DependencyInjection;

namespace IntelligentTaskAgent.MAF.Agents;

public sealed class UserProfileAgent : IUserProfileAgent
{
    public AgentType AgentType => AgentType.UserProfile;

    private readonly ChatClientAgent agent;

    private readonly IConversationMemory conversationMemory;

    public UserProfileAgent(
        IChatClient chatClient,
        IServiceProvider serviceProvider,
        IConversationMemory conversationMemory)
    {
        this.conversationMemory = conversationMemory;

        var userPlugin =
            serviceProvider.GetRequiredService<IUserProfilePlugin>();

        var tools = new List<AITool>
        {
            AIFunctionFactory.Create(
                userPlugin.GetUserByEmailAsync),

            AIFunctionFactory.Create(
                userPlugin.SearchUserAsync),

            AIFunctionFactory.Create(
                userPlugin.CreateUserAsync),

            AIFunctionFactory.Create(
                userPlugin.UpdateUserProfileAsync)
        };

        var prompt = PromptBuilder
            .Create("You are an Enterprise User Profile Assistant.")
            .WithCommon()
            .With(UserProfileInstructions.Prompt)
            .Build();

        agent = new ChatClientAgent(
            chatClient,
            instructions: prompt,
            name: "UserProfileAgent",
            description: "Enterprise User Profile Assistant",
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