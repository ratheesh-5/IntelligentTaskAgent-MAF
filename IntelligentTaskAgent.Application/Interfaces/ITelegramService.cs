using IntelligentTaskAgent.Core.Context;
using IntelligentTaskAgent.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntelligentTaskAgent.Application.Interfaces
{
    public interface ITelegramService
    {
        Task<TelegramOnboardingSession?> GetAsync(long chatId);
        Task UpsertAsync(TelegramOnboardingSession session);
        Task DeleteAsync(long chatId);

        Task<ConversationState?> GetConversationStateAsync(long chatId, CancellationToken ct = default);
        Task SaveConversationStateAsync(long chatId, ConversationState state, CancellationToken ct = default);
        Task DeleteConversationStateAsync(long chatId, CancellationToken ct = default);
    }
}
