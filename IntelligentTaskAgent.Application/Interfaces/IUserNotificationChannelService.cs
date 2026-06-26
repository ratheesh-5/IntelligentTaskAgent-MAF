using System;
using System.Collections.Generic;
using System.Text;
using IntelligentTaskAgent.Core.Domain.Entities;

namespace IntelligentTaskAgent.Application.Interfaces
{
    public interface IUserNotificationChannelService
    {
        Task AddOrUpdateChannelAsync(
            Guid userId,
            string channel,
            string channelValue,
            bool isPrimary);

        Task<IEnumerable<UserNotificationChannel>> GetByUserAsync(Guid userId);
    }
}
