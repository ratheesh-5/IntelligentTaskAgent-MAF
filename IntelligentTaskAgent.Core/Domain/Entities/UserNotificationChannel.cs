using System;
using System.Collections.Generic;
using System.Text;

namespace IntelligentTaskAgent.Core.Domain.Entities
{
    public class UserNotificationChannel
    {
        public Guid ChannelId { get; set; }
        public Guid UserId { get; set; }

        public string Channel { get; set; } = string.Empty;
        // Email, Telegram, SMS, WhatsApp

        public string ChannelValue { get; set; } = string.Empty;
        // email / telegram chatId / phone

        public bool IsPrimary { get; set; }
        public bool IsEnabled { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
