using System;
using System.Collections.Generic;
using System.Text;

namespace IntelligentTaskAgent.Core.Domain
{
    public class AgentExecutionLogEntity
    {
        public Guid LogId { get; set; } = Guid.NewGuid();

        // Optional reference to the related Task
        public Guid? TaskId { get; set; }

        // 🔑 Correlates agent + task lifecycle
        public Guid CorrelationId { get; set; }

        // Input given to the agent
        public string UserInput { get; set; } = string.Empty;

        // Output returned by the agent
        public string Output { get; set; } = string.Empty;

        // What the agent did (action/function)
        public string AgentAction { get; set; } = string.Empty;

        // Model used for this execution
        public string ModelUsed { get; set; } = string.Empty;

        // Whether execution was successful
        public bool Success { get; set; }

        // Error message if failed
        public string? ErrorMessage { get; set; }

        // Timestamp of execution
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Navigation property (optional)
        public TaskEntity? Task { get; set; }
    }

}
