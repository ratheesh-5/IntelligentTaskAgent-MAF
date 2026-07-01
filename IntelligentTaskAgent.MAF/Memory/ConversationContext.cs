using IntelligentTaskAgent.Core.Context;
using IntelligentTaskAgent.MAF.Enum;
using Microsoft.Agents.AI;
using System.Collections.Generic;

namespace IntelligentTaskAgent.MAF.Memory
{
    public sealed class ConversationContext
    {
        public string ConversationId { get; init; } = default!;

        public AgentSession? Session { get; set; }

        public Guid? UserId { get; set; }

        public string? Email { get; set; }

        public AgentType? ActiveAgent { get; set; }

        public AgentWorkflowState WorkflowState { get; set; }

        public string? PendingUserRequest { get; set; }
    }
}

