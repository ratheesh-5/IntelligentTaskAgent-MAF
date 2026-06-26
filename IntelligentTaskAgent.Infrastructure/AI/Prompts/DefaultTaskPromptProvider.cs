using IntelligentTaskAgent.Core.AI.Prompts;

public class DefaultTaskPromptProvider : ITaskPromptProvider
{
    public Task<string> BuildTaskExtractionPromptAsync(
        string userInput,
        DateTime nowUtc)
    {
        var prompt = $$"""
You are an AI assistant.

USER TIMEZONE: IST (UTC+05:30)
TODAY (IST): {{nowUtc:yyyy-MM-ddTHH:mm:ssZ}}
RETURN ReminderAt IN UTC ONLY

TASK:
Extract task reminder information and return STRICT JSON.

RULES (MANDATORY):
- Return JSON ONLY
- No explanation, no markdown
- Use ISO 8601 UTC datetime format: yyyy-MM-ddTHH:mm:ssZ
- Resolve relative dates like "today", "tomorrow", "next week" BASED ON TODAY
- Never return past dates
- If no reminder is mentioned, set "ReminderAt": null

CHANNEL RULES:
- If a notification channel is explicitly mentioned (Email, SMS, Telegram, WhatsApp), capture it
- If NO channel is mentioned, set "Channel": null
  (system will choose the user's preferred channel)

FIELD RULES (VERY IMPORTANT):
- TaskTitle MUST NOT be empty
- Description MUST NOT be empty

DESCRIPTION RULES:
- Description MUST be derived from the user input
- You MAY fix grammar and spelling only
- Do NOT change meaning
- Do NOT add new information

DESCRIPTION RULES (VERY IMPORTANT):
- Description MUST NOT contain date or time information
- Remove phrases like "today", "tomorrow", "by 5 PM", "at 8:30"

TITLE RULES:
- If the user does not provide a clear title:
  → Generate a short title (max 6 words)

Input:
"{{userInput}}"

Return JSON only:
{
  "Intent": "CreateTask",
  "TaskTitle": "string",
  "Description": "string",
  "ReminderAt": "yyyy-MM-ddTHH:mm:ssZ | null",
  "Channel": "Email | SMS | Telegram | WhatsApp | null"
}
""";
        return Task.FromResult(prompt);
    }
}