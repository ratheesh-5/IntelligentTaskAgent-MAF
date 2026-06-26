using System;
using System.Collections.Generic;
using System.Text;

namespace IntelligentTaskAgent.Core.AI.Models
{
    public class TaskIntentResult
    {
        public string Intent { get; set; } = string.Empty;
        public string TaskTitle { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime? ReminderAt { get; set; }
        public string Channel { get; set; } = string.Empty;

    }
}
