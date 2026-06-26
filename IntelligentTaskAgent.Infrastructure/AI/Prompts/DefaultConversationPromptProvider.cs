using IntelligentTaskAgent.Core.AI.Prompts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntelligentTaskAgent.Infrastructure.AI.Prompts
{
    public class DefaultConversationPromptProvider : IConversationPromptProvider
    {
        public Task<string> BuildIntentClassificationPromptAsync(string userInput)
        {
            var prompt = $$"""
            You are an intent classification agent for a reminder application.
            
            Classify the user message into exactly ONE of the following decisions:
            
            - TaskIntent → creating, updating, querying reminders or tasks
            - UserProfile → updating email or notification channel
            - Unsupported → WhatsApp, SMS, or unsupported channels
            - OutOfScope → unrelated questions (e.g., "what is python")
            
            User message:
            "{{userInput}}"
            
            Respond ONLY in valid JSON:
            
            {
              "Decision": "TaskIntent | UserProfile | Unsupported | OutOfScope"
            }
            """;
                        return Task.FromResult(prompt);
        }
    }
}
