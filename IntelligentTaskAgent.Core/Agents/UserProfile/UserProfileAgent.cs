using IntelligentTaskAgent.Core.AI.Models;
using IntelligentTaskAgent.Core.AI.Parsers;
using IntelligentTaskAgent.Core.AI.Prompts;
using IntelligentTaskAgent.Core.Context;
using IntelligentTaskAgent.Core.Interfaces;
using IntelligentTaskAgent.Core.Responses;
using System.Text.Json;

namespace IntelligentTaskAgent.Core.Agents.UserProfile;

public class UserProfileAgent : IUserProfileAgent
{
    private readonly ILLMClient llm;
    private readonly IUserProfilePromptProvider userProfilePromptProvider;
    private readonly ILLMResponseParser<UserProfileExtraction> responseParser;

    public UserProfileAgent(
        ILLMClient llm
        , IUserProfilePromptProvider userProfilePromptProvider
        , ILLMResponseParser<UserProfileExtraction> responseParser)
    {
        this.llm = llm;
        this.userProfilePromptProvider = userProfilePromptProvider; 
        this.responseParser = responseParser;
    }

    public async Task<AgentResponse> HandleAsync(
    AgentContext context,
    ConversationState state)
    {
        var prompt = await userProfilePromptProvider
            .BuildIserProfileExtractionPromptAsync(context.Message);

        var raw = await llm.CompleteAsync(prompt);

        UserProfileExtraction? result;
        try
        {
            //result = JsonSerializer.Deserialize<UserProfileExtraction>(raw);
            result = responseParser.Parse(raw);
        }
        catch
        {
            return AgentResponse.NotHandled();
        }

        if (!string.IsNullOrWhiteSpace(result?.PreferredChannel))
        {
            state.PreferredChannel = result.PreferredChannel;
        }

        if (state.Stage == ConversationStage.AwaitingEmail &&
            string.IsNullOrWhiteSpace(result?.Email))
        {
            return AgentResponse.Reply(
                "I still need your email address to continue 🙂");
        }

        if (!string.IsNullOrWhiteSpace(result?.Email))
        {
            state.Email = result.Email;
            state.Stage = ConversationStage.None;

            return AgentResponse.Reply(
                $"Thanks 👍 I’ve saved your email: {result.Email}");
        }

        return AgentResponse.NotHandled();
    }

    public sealed class UserProfileExtraction
    {
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string? PreferredChannel { get; set; } // Email | Telegram
    }
}
