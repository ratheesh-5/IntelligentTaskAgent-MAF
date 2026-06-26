using IntelligentTaskAgent.Notifications.Domain.Entities;
using IntelligentTaskAgent.Notifications.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IntelligentTaskAgent.Repositories.DbContexts;

namespace IntelligentTaskAgent.Repositories.Services
{
    public class NotificationLogRepository : INotificationLogRepository
    {
        private readonly IntelligentTaskAgentDbContext db;

        public NotificationLogRepository(IntelligentTaskAgentDbContext db)
        {
            this.db = db;
        }
        public async Task AddAsync(NotificationLog log)
        {
            db.NotificationLog.Add(log);
            await db.SaveChangesAsync();
        }
    }
}
