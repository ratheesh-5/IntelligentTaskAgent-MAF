using IntelligentTaskAgent.Repositories.DbContexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IntelligentTaskAgent.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using IntelligentTaskAgent.Core.Interfaces;
namespace IntelligentTaskAgent.Repositories.Services
{
    public sealed class TelegramOnboardingSessionRepository : ITelegramOnboardingSessionRepository
    {
        private readonly IntelligentTaskAgentDbContext intelligentTaskAgentDbContext;

        public TelegramOnboardingSessionRepository(IntelligentTaskAgentDbContext intelligentTaskAgentDbContext)
        {
            this.intelligentTaskAgentDbContext = intelligentTaskAgentDbContext;
        }
        public async Task<TelegramOnboardingSession?> GetAsync(long chatId)
        {
           return await this.intelligentTaskAgentDbContext.TelegramOnboardingSession.FirstOrDefaultAsync(x => x.ChatId == chatId);
        }

        public async Task UpsertAsync(TelegramOnboardingSession session)
        {
            var existing = await this.intelligentTaskAgentDbContext.TelegramOnboardingSession.FirstOrDefaultAsync(x => x.ChatId == session.ChatId);
            if (existing == null)
            {
                this.intelligentTaskAgentDbContext.TelegramOnboardingSession.Add(session);
            }
            else
            {
                existing.State = session.State;
            }
            await this.intelligentTaskAgentDbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(long chatId)
        {
            var existing = await this.intelligentTaskAgentDbContext.TelegramOnboardingSession.FirstOrDefaultAsync(x => x.ChatId == chatId);
            if (existing != null)
            {
                this.intelligentTaskAgentDbContext.TelegramOnboardingSession.Remove(existing);
                await this.intelligentTaskAgentDbContext.SaveChangesAsync();
            }
        }
    }


}
