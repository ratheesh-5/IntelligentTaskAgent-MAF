using IntelligentTaskAgent.Core.AI.Models;
using IntelligentTaskAgent.Core.AI.Parsers;
using IntelligentTaskAgent.Core.AI.Prompts;
using IntelligentTaskAgent.Core.Context;
using IntelligentTaskAgent.Core.Interfaces;
using IntelligentTaskAgent.Core.Responses;
using IntelligentTaskAgent.Core.Services;

namespace IntelligentTaskAgent.Core.Agents.TaskIntent;

public class TaskIntentAgent : ITaskIntentAgent
{
    private readonly ILLMResponseParser<TaskIntentResult> responseParser;
    private readonly ILLMClient llmClient;
    private readonly ITaskPromptProvider taskPromptProvider;

    public TaskIntentAgent(
        ILLMResponseParser<TaskIntentResult> responseParser,
        AgentLoggingService agentLoggingService,
        ILLMClient llmClient,
        ITaskPromptProvider taskPromptProvider)
    {
        this.responseParser = responseParser;
        this.llmClient = llmClient;
        this.taskPromptProvider = taskPromptProvider;
    }

    public async Task<AgentResponse> HandleAsync(
    AgentContext context,
    ConversationState state)
    {
        try
        {
            var prompt = await taskPromptProvider
                .BuildTaskExtractionPromptAsync(
                    context.Message,
                    DateTime.UtcNow);

            var raw = await llmClient.CompleteAsync(prompt);
            var intent = responseParser.Parse(raw);

            if (intent.Intent == "Unknown")
            {
                return AgentResponse.Reply(
                    "I couldn’t understand the task. Can you rephrase?");
            }

            // ✅ Update conversation state only
            state.IsTaskIntentConfirmed = true;
            state.Stage = ConversationStage.Completed;

            // ✅ Store extracted intent temporarily in context/state if needed later
            state.ExtractedTaskIntent = intent; // see note below

            return AgentResponse.Reply(
                $"Got it 👍 I’ve noted your task: \"{intent.TaskTitle}\"");
        }
        catch
        {
            return AgentResponse.Reply(
                "Something went wrong while understanding your task.");
        }
    }
}