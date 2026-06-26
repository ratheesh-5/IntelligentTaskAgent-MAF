using System;
using System.Collections.Generic;
using System.Text;

namespace IntelligentTaskAgent.Notifications.Domain.Interfaces
{
    public interface INotificationChannelRepository
    {
        Task<int> GetChannelIdByNameAsync(string channelName);
    }
}
