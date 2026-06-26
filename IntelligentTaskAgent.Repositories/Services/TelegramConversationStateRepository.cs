using IntelligentTaskAgent.Core.Context;
using IntelligentTaskAgent.Core.Domain.Entities;
using IntelligentTaskAgent.Core.Interfaces;
using IntelligentTaskAgent.Repositories.DbContexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntelligentTaskAgent.Repositories.Services
{
    public class TelegramConversationStateRepository : ITelegramConversationStateRepository
    {
        private readonly IntelligentTaskAgentDbContext intelligentTaskAgentDbContext;

        public TelegramConversationStateRepository(IntelligentTaskAgentDbContext intelligentTaskAgentDbContext)
        {
            this.intelligentTaskAgentDbContext = intelligentTaskAgentDbContext;
        }
        public async Task<TelegramConversationStateEntity?> GetAsync(
                                                    long chatId,
                                        CancellationToken ct = default)
        {
            return await intelligentTaskAgentDbContext.TelegramConversationStates
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.ChatId == chatId, ct);
        }


        public async Task UpsertAsync(
                            TelegramConversationStateEntity entity,
                                CancellationToken ct = default)
        {
            var existing = await intelligentTaskAgentDbContext.TelegramConversationStates
                .FirstOrDefaultAsync(x => x.ChatId == entity.ChatId, ct);

            if (existing == null)
            {
                entity.UpdatedAtUtc = DateTime.UtcNow;

                await intelligentTaskAgentDbContext.TelegramConversationStates
                    .AddAsync(entity, ct);
            }
            else
            {
                existing.StateJson = entity.StateJson;
                existing.UpdatedAtUtc = DateTime.UtcNow;

                // Explicitly mark fields as modified (safe update)
                intelligentTaskAgentDbContext.Entry(existing).State = EntityState.Modified;
            }

            await intelligentTaskAgentDbContext.SaveChangesAsync(ct);
        }


        public async Task DeleteAsync(
    long chatId,
    CancellationToken ct = default)
        {
            var entity = await intelligentTaskAgentDbContext.TelegramConversationStates
                .FirstOrDefaultAsync(x => x.ChatId == chatId, ct);

            if (entity == null)
                return;

            intelligentTaskAgentDbContext.TelegramConversationStates.Remove(entity);
            await intelligentTaskAgentDbContext.SaveChangesAsync(ct);
        }
    }
}
