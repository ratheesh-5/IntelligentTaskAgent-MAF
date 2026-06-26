using IntelligentTaskAgent.Core.Domain;
using IntelligentTaskAgent.Core.Interfaces;
using IntelligentTaskAgent.Repositories.DbContexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace IntelligentTaskAgent.Repositories.Services
{
    public class AgentExecutionLogRepository : IAgentExecutionLogRepository
    {
        private readonly IntelligentTaskAgentDbContext intelligentTaskAgentDbContext;
        public AgentExecutionLogRepository(IntelligentTaskAgentDbContext intelligentTaskAgentDbContext)
        {
            this.intelligentTaskAgentDbContext = intelligentTaskAgentDbContext;
        }
        public async Task AddAsync(AgentExecutionLogEntity log)
        {
            await this.intelligentTaskAgentDbContext.AgentExecutionLogs.AddAsync(log);
            await this.intelligentTaskAgentDbContext.SaveChangesAsync();
        }

        public async Task UpdateTaskIdByCorrelationAsync(
            Guid correlationId,
            Guid taskId)
        {
            var logs = await this.intelligentTaskAgentDbContext.AgentExecutionLogs
                .Where(l => l.CorrelationId == correlationId)
                .ToListAsync();

            foreach (var log in logs)
            {
                log.TaskId = taskId;
            }

            await this.intelligentTaskAgentDbContext.SaveChangesAsync();
        }

    }
}
