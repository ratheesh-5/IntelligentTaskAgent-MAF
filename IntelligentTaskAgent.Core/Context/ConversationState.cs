using IntelligentTaskAgent.Core.AI.Models;

namespace IntelligentTaskAgent.Core.Context;

public class ConversationState
{
    // 🔁 Where we are in the flow
    public ConversationStage Stage { get; set; } = ConversationStage.None;

    // 🧠 Extracted task (temporary memory)
    public TaskIntentResult? ExtractedTaskIntent { get; set; }

    // 👤 User profile (temporary)
    public string? Email { get; set; }
    public string? PhoneNumber { get; set; }
    public string? PreferredChannel { get; set; } // Email, Telegram

    // ✅ Task confirmation
    public bool IsTaskIntentConfirmed { get; set; }

    // 📝 Last user message
    public string? LastUserMessage { get; set; }

    // 🔎 Helpers
    public bool HasEmail => !string.IsNullOrWhiteSpace(Email);
    public bool HasPhone => !string.IsNullOrWhiteSpace(PhoneNumber);
    public bool HasChannel => !string.IsNullOrWhiteSpace(PreferredChannel);
}



