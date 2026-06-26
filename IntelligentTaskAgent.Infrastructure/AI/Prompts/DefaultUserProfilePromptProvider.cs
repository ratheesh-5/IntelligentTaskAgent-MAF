using IntelligentTaskAgent.Core.AI.Prompts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntelligentTaskAgent.Infrastructure.AI.Prompts
{
    public class DefaultUserProfilePromptProvider : IUserProfilePromptProvider
    {
        public Task<string> BuildIserProfileExtractionPromptAsync(string userInput)
        {
            // TODO : Update the query based on UserProfileExtraction class
            var prompt = $$"""
            You are a user profile extraction agent.
            
            Extract the following details from the user message **only if present**:
            - Email address
            - Phone number (any common international or local format)
            - Preferred notification channel (only "Email" or "Telegram")
            
            Rules:
            - If a field is not present, return null for that field
            - Do NOT guess or infer values
            - Do NOT add extra text
            - Do NOT include explanations
            - PreferredChannel must be exactly "Email" or "Telegram" (case-sensitive)
            
            User message:
            "{{userInput}}"
            
            Respond ONLY in valid JSON:
            
            {
              "Email": "string | null",
              "PhoneNumber": "string | null",
              "PreferredChannel": "Email | Telegram | null"
            }
            """;

            return Task.FromResult(prompt);
        }
    }
}
