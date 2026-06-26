using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntelligentTaskAgent.Core.Domain.Entities
{
    public sealed class TelegramOnboardingSession
    {
        public long ChatId { get; set; }
        public string State { get; set; } = "WaitingForEmail";
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
