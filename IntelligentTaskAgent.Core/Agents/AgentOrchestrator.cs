using IntelligentTaskAgent.Core.Agents.Conversation;
using IntelligentTaskAgent.Core.Agents.TaskIntent;
using IntelligentTaskAgent.Core.Agents.UserProfile;
using IntelligentTaskAgent.Core.Context;
using IntelligentTaskAgent.Core.Responses;
using IntelligentTaskAgent.Core.Services;
using System.ComponentModel.Design.Serialization;

public class AgentOrchestrator
{
    private readonly IConversationAgent conversationAgent;
    private readonly IUserProfileAgent userProfileAgent;
    private readonly ITaskIntentAgent taskIntentAgent;
    private readonly TaskOrchestrationService taskOrchestrationService;

    public AgentOrchestrator(
        IConversationAgent conversationAgent,
        IUserProfileAgent userProfileAgent,
        ITaskIntentAgent taskIntentAgent,
        TaskOrchestrationService taskOrchestrationService)
    {
        this.conversationAgent = conversationAgent;
        this.userProfileAgent = userProfileAgent;
        this.taskIntentAgent = taskIntentAgent;
        this.taskOrchestrationService = taskOrchestrationService;
    }

    public async Task<AgentResponse> HandleAsync(
        AgentContext context,
        ConversationState state)
    {
        state.LastUserMessage = context.Message;

        // 🔹 1. If waiting for email → force profile agent
        if (state.Stage == ConversationStage.AwaitingEmail)
        {
            return await userProfileAgent.HandleAsync(context, state);
        }

        // 🔹 2. Conversation routing
        var convo = await conversationAgent.HandleAsync(context, state);
        if (convo.Handled)
            return convo;

        // 🔹 3. Ensure user profile
        if (!state.HasEmail)
        {
            state.Stage = ConversationStage.AwaitingEmail;
            return AgentResponse.Reply(
                "Before I proceed, may I have your email?");
        }

        // 🔹 4. Task intent extraction
        var taskResponse = await taskIntentAgent.HandleAsync(context, state);
        if (!taskResponse.Handled)
            return taskResponse;

        // 5️⃣ Mark completion ONLY
        if (state.ExtractedTaskIntent != null)
        {
            state.Stage = ConversationStage.Completed;
        }
        
        return AgentResponse.Reply(
            "✅ Your task has been saved and I’ll remind you as requested.");
    }
}
