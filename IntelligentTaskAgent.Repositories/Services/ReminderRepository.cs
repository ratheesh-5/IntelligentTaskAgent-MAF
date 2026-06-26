using IntelligentTaskAgent.Core.Domain;
using IntelligentTaskAgent.Core.Interfaces;
using IntelligentTaskAgent.Repositories.DbContexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace IntelligentTaskAgent.Repositories.Services
{
    public class ReminderRepository : IReminderRepository
    {
        private readonly IntelligentTaskAgentDbContext intelligentTaskAgentDbContext;

        public ReminderRepository(IntelligentTaskAgentDbContext intelligentTaskAgentDbContext)
        {
            this.intelligentTaskAgentDbContext = intelligentTaskAgentDbContext;
        }
        public async Task AddAsync(ReminderEntity reminder)
        {
            await this.intelligentTaskAgentDbContext.Reminders.AddAsync(reminder);
            await this.intelligentTaskAgentDbContext.SaveChangesAsync();
        }
        public async Task UpdateAsync(ReminderEntity reminder)
        {
            this.intelligentTaskAgentDbContext.Reminders.Update(reminder);
            await this.intelligentTaskAgentDbContext.SaveChangesAsync();
        }
        public async Task<IEnumerable<ReminderEntity>> GetPendingAsync(DateTime utcNow)
        {
            var result =  await this.intelligentTaskAgentDbContext.Reminders
                                    .Where(r => r.SentAt == null && r.NotifyAt <= utcNow)
                                    .ToListAsync();
            return result;
        }
    }
}
