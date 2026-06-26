using IntelligentTaskAgent.Core.Domain;
using IntelligentTaskAgent.Core.Interfaces;
using IntelligentTaskAgent.Repositories.DbContexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace IntelligentTaskAgent.Repositories.Services
{
    public class TaskRepository : ITaskRepository
    {
        private readonly IntelligentTaskAgentDbContext intelligentTaskAgentDbContext;

        public TaskRepository(IntelligentTaskAgentDbContext intelligentTaskAgentDbContext)
        {
            this.intelligentTaskAgentDbContext = intelligentTaskAgentDbContext;
        }
        public async Task AddAsync(TaskEntity taskEntity)
        {
            await this.intelligentTaskAgentDbContext.Tasks.AddAsync(taskEntity);
            await this.intelligentTaskAgentDbContext.SaveChangesAsync();
        }

        public async Task<TaskEntity?> SearchByTaskIdAsync(Guid taskId)
        {
            return await this.intelligentTaskAgentDbContext.Tasks
                .FirstOrDefaultAsync(t => t.TaskId == taskId);
        }

        public async Task<List<TaskEntity>> SearchByTaskTitleAsync(string title)
        {
            if (string.IsNullOrWhiteSpace(title))
            {
                return new List<TaskEntity>();
            }

            title = title.Trim();

            return await this.intelligentTaskAgentDbContext.Tasks
                .Where(t => t.Title.Contains(title))
                .ToListAsync();
        }

        public async Task UpdateAsync(TaskEntity taskEntity)
        {
            this.intelligentTaskAgentDbContext.Tasks.Update(taskEntity);
            await this.intelligentTaskAgentDbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid taskId)
        {
            var task = await this.intelligentTaskAgentDbContext.Tasks
                .FirstOrDefaultAsync(t => t.TaskId == taskId);

            if (task == null)
            {
                return;
            }

            this.intelligentTaskAgentDbContext.Tasks.Remove(task);
            await this.intelligentTaskAgentDbContext.SaveChangesAsync();
        }

        public async Task<List<TaskEntity>> SearchAsync(string keyword)
        {
            if (string.IsNullOrWhiteSpace(keyword))
            {
                return new List<TaskEntity>();
            }

            keyword = keyword.Trim();

            return await this.intelligentTaskAgentDbContext.Tasks
                .Where(t =>
                    t.Title.Contains(keyword) ||
                    t.Description.Contains(keyword))
                .ToListAsync();
        }
    }
}
