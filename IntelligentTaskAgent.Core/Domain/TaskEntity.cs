using System;
using System.Collections.Generic;
using System.Text;

namespace IntelligentTaskAgent.Core.Domain
{
    public class TaskEntity
    {
        public Guid TaskId { get; set; }

        public string Title { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        // Nullable because reminder may not exist
        public DateTime? DueDate { get; set; }

        public string Status { get; set; } = "Pending";

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime? CompletedAt { get; set; }

        public string? RawUserInput { get; set; } = string.Empty;
    }
}
