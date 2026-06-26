using IntelligentTaskAgent.Application.Interfaces;
using IntelligentTaskAgent.Core.Domain.Entities;
using IntelligentTaskAgent.Core.Interfaces;
using Microsoft.SemanticKernel;
using System;
using System.Collections.Generic;
using System.Text;

namespace IntelligentTaskAgent.Application.Services
{
    public class UserNotificationChannelService : IUserNotificationChannelService
    {
        private readonly IUserNotificationChannelRepository repository;

        public UserNotificationChannelService(
            IUserNotificationChannelRepository repository)
        {
            this.repository = repository;
        }
        public async Task AddOrUpdateChannelAsync(
            Guid userId,
            string channel,
            string channelValue,
            bool isPrimary)
        {
            var existing = (await repository.GetByUserAsync(userId))
                .FirstOrDefault(x => x.Channel == channel);

            if (existing != null)
            {
                existing.ChannelValue = channelValue;
                existing.IsPrimary = isPrimary;
                existing.IsEnabled = true;

                await repository.UpdateAsync(existing);
                return;
            }

            var entity = new UserNotificationChannel
            {
                ChannelId = Guid.NewGuid(),
                UserId = userId,
                Channel = channel,
                ChannelValue = channelValue,
                IsPrimary = isPrimary,
                IsEnabled = true,
                CreatedAt = DateTime.UtcNow
            };

            await repository.AddAsync(entity);
        }

        public async Task<IEnumerable<UserNotificationChannel>> GetByUserAsync(Guid userId)
        {
            return await repository.GetByUserAsync(userId);
        }
    }
}
