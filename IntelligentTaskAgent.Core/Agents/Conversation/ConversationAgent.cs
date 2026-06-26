using IntelligentTaskAgent.Core.AI.Models;
using IntelligentTaskAgent.Core.AI.Parsers;
using IntelligentTaskAgent.Core.AI.Prompts;
using IntelligentTaskAgent.Core.Context;
using IntelligentTaskAgent.Core.Interfaces;
using IntelligentTaskAgent.Core.Responses;
using System.Text.Json;

namespace IntelligentTaskAgent.Core.Agents.Conversation;

public class ConversationAgent : IConversationAgent
{
    private readonly ILLMClient llm;
    private readonly IConversationPromptProvider conversationPromptProvider;

    private readonly ILLMResponseParser<ConversationResult> responseParser;

    public ConversationAgent(ILLMClient llm
        , IConversationPromptProvider conversationPromptProvider
        , ILLMResponseParser<ConversationResult> responseParser)
    {
        this.llm = llm;
        this.conversationPromptProvider = conversationPromptProvider;
        this.responseParser = responseParser;
    }

    public async Task<AgentResponse> HandleAsync(
        AgentContext context,
        ConversationState state)
    {
        var prompt = await this.conversationPromptProvider.BuildIntentClassificationPromptAsync(context.Message);

        var llmResponse = await llm.CompleteAsync(prompt);

        ConversationDecision decision;
        try
        {
            //var result = JsonSerializer.Deserialize<ConversationResult>(llmResponse);
            var result = this.responseParser.Parse(llmResponse);
            decision = result!.Decision;
        }
        catch
        {
            return AgentResponse.Reply(
                "Sorry, I couldn’t understand that. Please try again.");
        }

        switch (decision)
        {
            case ConversationDecision.TaskIntent:
                state.Stage = ConversationStage.AwaitingTaskConfirmation;
                return AgentResponse.NotHandled();
            case ConversationDecision.UserProfile:
                return AgentResponse.NotHandled();
            case ConversationDecision.Unsupported:
                return AgentResponse.Reply(
                    "Currently I support Email and Telegram notifications only 🙂");
            case ConversationDecision.OutOfScope:
                return AgentResponse.Reply(
                    "I can help with reminders and tasks. Try:\n“Remind me to call John at 9 PM”");
            default:
                return AgentResponse.NotHandled();
        }   
    }
    public sealed class ConversationResult
    {
        public ConversationDecision Decision { get; set; }
    }
}