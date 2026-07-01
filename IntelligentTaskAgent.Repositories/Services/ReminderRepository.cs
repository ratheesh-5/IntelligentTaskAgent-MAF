using IntelligentTaskAgent.Core.Domain;
using IntelligentTaskAgent.Core.Interfaces;
using IntelligentTaskAgent.Core.RepositoryModels;
using IntelligentTaskAgent.Repositories.DbContexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.SemanticKernel;
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

        public async Task DeleteReminderAsync(Guid taskId)
        {
            var reminder = await this.intelligentTaskAgentDbContext.Reminders.FirstOrDefaultAsync(r => r.TaskId == taskId);
            if (reminder != null)
            {
                this.intelligentTaskAgentDbContext.Reminders.Remove(reminder);
                await this.intelligentTaskAgentDbContext.SaveChangesAsync();
            }
        }

        public async Task<ReminderEntity?> SearchByTaskIdAsync(Guid taskId)
        {
            return await this.intelligentTaskAgentDbContext.Reminders
                .FirstOrDefaultAsync(r => r.TaskId == taskId);
        }

        public async Task<List<ReminderProjection>> SearchAsync(
    ReminderSearchCriteria criteria)
        {
            var query =
     from reminder in intelligentTaskAgentDbContext.Reminders
     join task in intelligentTaskAgentDbContext.Tasks
         on reminder.TaskId equals task.TaskId
     select new
     {
         Reminder = reminder,
         Task = task
     };

            if (criteria.UserId.HasValue)
            {
                query = query.Where(x =>
                    x.Reminder.UserId == criteria.UserId.Value);
            }

            if (!string.IsNullOrWhiteSpace(criteria.Keyword))
            {
                query = query.Where(x =>
                    x.Task.Title.Contains(criteria.Keyword) ||
                    x.Task.Description.Contains(criteria.Keyword));
            }

            if (!string.IsNullOrWhiteSpace(criteria.Status))
            {
                query = query.Where(x =>
                    x.Task.Status == criteria.Status);
            }

            // Search by DueDate
            if (criteria.FromDate.HasValue)
            {
                query = query.Where(x =>
                    x.Task.DueDate >= criteria.FromDate.Value);
            }

            if (criteria.ToDate.HasValue)
            {
                query = query.Where(x =>
                    x.Task.DueDate <= criteria.ToDate.Value);
            }

            query = query.OrderBy(x => x.Task.DueDate);

            if (criteria.Top.HasValue)
            {
                query = query.Take(criteria.Top.Value);
            }

            return await query
                .Select(x => new ReminderProjection
                {
                    TaskId = x.Task.TaskId,
                    ReminderId = x.Reminder.ReminderId,
                    Title = x.Task.Title,
                    Description = x.Task.Description,
                    ReminderAt = x.Reminder.NotifyAt,
                    Status = x.Task.Status,
                    Channel = x.Reminder.Channel
                })
                .ToListAsync();
        }

    }
}
