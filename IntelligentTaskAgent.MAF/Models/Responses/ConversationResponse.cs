using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntelligentTaskAgent.MAF.Models.Responses
{
    public sealed class ConversationResponse
    {
        public string Message { get; set; } = string.Empty;

        public string? ConversationId { get; set; }

        public string? AgentId { get; set; }

        public DateTimeOffset? CreatedAt { get; set; }
    }
}
