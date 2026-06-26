using IntelligentTaskAgent.Telegram.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntelligentTaskAgent.Telegram.Handlers
{
    public interface ITelegramConversationHandler
    {
        Task HandleAsync(TelegramUpdate update, CancellationToken ct = default);
    }
}
