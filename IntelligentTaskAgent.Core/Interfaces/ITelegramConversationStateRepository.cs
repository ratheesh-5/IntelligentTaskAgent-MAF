using IntelligentTaskAgent.Core.Context;
using IntelligentTaskAgent.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntelligentTaskAgent.Core.Interfaces
{
    public interface ITelegramConversationStateRepository
    {
        Task<TelegramConversationStateEntity?> GetAsync(long chatId, CancellationToken ct);
        Task UpsertAsync(TelegramConversationStateEntity entity, CancellationToken ct);
        Task DeleteAsync(long chatId, CancellationToken ct);
    }
}
