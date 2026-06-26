using IntelligentTaskAgent.Notifications.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace IntelligentTaskAgent.Notifications.Domain.Interfaces
{
    public interface INotificationLogRepository
    {
        Task AddAsync(NotificationLog log);
    }
}
