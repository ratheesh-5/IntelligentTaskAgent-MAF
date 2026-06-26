using IntelligentTaskAgent.Core.Context;
using IntelligentTaskAgent.Core.Responses;

namespace IntelligentTaskAgent.Core.Agents.Conversation;

public interface IConversationAgent
{
    Task<AgentResponse> HandleAsync(
        AgentContext context,
        ConversationState state);
}
