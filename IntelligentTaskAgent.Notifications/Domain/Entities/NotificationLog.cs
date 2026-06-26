using IntelligentTaskAgent.Notifications.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace IntelligentTaskAgent.Notifications.Domain.Entities
{
    public class NotificationLog
    {
        public Guid NotificationLogId { get; set; }
        public Guid UserId { get; set; }
        public NotificationChannelType ChannelType { get; set; }
        public string Destination { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
        public bool IsSuccess { get; set; }
        public string? ErrorMessage { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
