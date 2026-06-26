using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntelligentTaskAgent.Core.Domain.Entities
{
    public class TelegramConversationStateEntity
    {
        public long ChatId { get; set; }
        public string StateJson { get; set; } = string.Empty;
        public DateTime UpdatedAtUtc { get; set; }
    }
}
