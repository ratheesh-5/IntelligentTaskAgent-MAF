using System;
using System.Collections.Generic;
using System.Text;
using IntelligentTaskAgent.Core.Domain.Enums;

namespace IntelligentTaskAgent.Core.Domain
{
    public class ReminderEntity
    {
        public Guid ReminderId { get; set; }
        public Guid UserId { get; set; }
        public Guid? TaskId { get; set; }
        public DateTime NotifyAt { get; set; }
        public string Message { get; set; } = string.Empty;
        public string? Channel { get; set; } = string.Empty;
        public bool IsSent { get; set; }

        public DateTime? SentAt { get; set; }
        public void MarkAsSent()
        {
            IsSent = true;
            SentAt = DateTime.UtcNow;
        }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
