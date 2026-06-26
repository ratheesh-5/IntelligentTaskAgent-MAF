using IntelligentTaskAgent.Notifications.Domain.Interfaces;
using IntelligentTaskAgent.Repositories.DbContexts;
using System;
using System.Collections.Generic;
using System.Text;

namespace IntelligentTaskAgent.Repositories.Services
{
    public class NotificationChannelRepository
    : INotificationChannelRepository
    {
        private readonly IntelligentTaskAgentDbContext db;

        public NotificationChannelRepository(IntelligentTaskAgentDbContext db)
        {
            this.db = db;
        }
        public async Task<int> GetChannelIdByNameAsync(string channelName)
        {
            // it's not required because don't follow channels table
            //return await this.db.NotificationChannels
            //    .Where(c => c.ChannelName == channelName)
            //    .Select(c => c.ChannelId)
            //    .FirstAsync();
            return 1;
        }
    }
}
