using System.Collections.Generic;

namespace IntelligentTaskAgent.MAF.Models.Conversation
{
    public class ConversationContext
    {
        public string UserId { get; set; } = string.Empty;
        public string Input { get; set; } = string.Empty;
        public IDictionary<string, object?> Properties { get; } = new Dictionary<string, object?>();
    }
}
