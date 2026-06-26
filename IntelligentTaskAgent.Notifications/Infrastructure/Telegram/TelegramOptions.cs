using System;
using System.Collections.Generic;
using System.Text;

namespace IntelligentTaskAgent.Notifications.Infrastructure.Telegram
{
    public class TelegramOptions
    {
        public const string SectionName = "Channel:Telegram";
        public string? BaseUrl { get; set; } 
        public string BotToken { get; set; } = string.Empty;
    }
}
