using IntelligentTaskAgent.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace IntelligentTaskAgent.Core.Interfaces
{
    public interface IUserNotificationChannelRepository
    {
        Task AddAsync(UserNotificationChannel channel);
        Task UpdateAsync(UserNotificationChannel channel);

        Task<IEnumerable<UserNotificationChannel>> GetByUserAsync(Guid userId);
        Task<IEnumerable<UserNotificationChannel>> GetEnabledAsync(Guid userId);

        Task<UserNotificationChannel?> GetPrimaryAsync(Guid userId);

        Task<UserNotificationChannel?> GetByChannelAsync(
                string channel,
                string channelValue);
    }
}
