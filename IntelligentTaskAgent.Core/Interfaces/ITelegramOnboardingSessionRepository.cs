using IntelligentTaskAgent.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntelligentTaskAgent.Core.Interfaces
{
    public interface ITelegramOnboardingSessionRepository
    {
        Task<TelegramOnboardingSession?> GetAsync(long chatId);
        Task UpsertAsync(TelegramOnboardingSession session);
        Task DeleteAsync(long chatId);
    }
}
