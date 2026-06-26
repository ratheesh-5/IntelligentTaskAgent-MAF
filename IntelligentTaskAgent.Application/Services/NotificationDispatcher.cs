using IntelligentTaskAgent.Core.Domain.Entities;
using IntelligentTaskAgent.Core.Interfaces;
using IntelligentTaskAgent.Notifications.Domain.Entities;
using IntelligentTaskAgent.Notifications.Domain.Enums;
using IntelligentTaskAgent.Notifications.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IntelligentTaskAgent.Application.Services
{
    public class NotificationDispatcher
    {
        private readonly IDictionary<NotificationChannelType, INotificationSender> senders;
        private readonly IUserNotificationChannelRepository channelRepository;
        private readonly INotificationLogRepository logRepository;

        private readonly Dictionary<Guid, List<UserNotificationChannel>> channelCache = new();

        public NotificationDispatcher(
            IEnumerable<INotificationSender> senders,
            IUserNotificationChannelRepository channelRepository,
            INotificationLogRepository logRepository)
        {
            this.senders = senders.ToDictionary(s => s.ChannelType);
            this.channelRepository = channelRepository;
            this.logRepository = logRepository;
        }

        public async Task DispatchWithFallbackAsync(
            Guid userId,
            string message,
            string? subject = null,
            string? requiredChannel = null)
        {
            var channels = await GetChannelsAsync(userId);

            if (!channels.Any())
            {
                throw new InvalidOperationException(
                    $"Required channel '{requiredChannel}' not enabled for user {userId}");
            }

            if (!string.IsNullOrWhiteSpace(requiredChannel))
            {
                channels = channels
                    .Where(c => c.Channel.Equals(requiredChannel, StringComparison.OrdinalIgnoreCase))
                    .ToList();
            }
            else
            {
                channels = channels
                    .OrderByDescending(c => c.IsPrimary)
                    .ToList();
            }

            foreach (var channel in channels)
            {
                NotificationChannelType channelType;

                try
                {
                    channelType = MapToNotificationEnum(channel.Channel);
                }
                catch
                {
                    // Unsupported channel – skip silently
                    continue;
                }

                if (!senders.TryGetValue(channelType, out var sender))
                    continue;

                try
                {
                    await sender.SendAsync(
                        channel.ChannelValue,
                        message:message,
                        subject);

                    await logRepository.AddAsync(new NotificationLog
                    {
                        UserId = userId,
                        ChannelType = channelType,
                        Destination = channel.ChannelValue,
                        Message = message,
                        IsSuccess = true
                    });

                    return; // Stop after first successful send
                }
                catch (Exception ex)
                {
                    await logRepository.AddAsync(new NotificationLog
                    {
                        UserId = userId,
                        ChannelType = channelType,
                        Destination = channel.ChannelValue,
                        Message = message,
                        IsSuccess = false,
                        ErrorMessage = ex.Message
                    });
                }
            }

            throw new InvalidOperationException("All notification channels failed");
        }

        private static NotificationChannelType MapToNotificationEnum(string channel)
        {
            return channel switch
            {
                "Email" => NotificationChannelType.Email,
                "Telegram" => NotificationChannelType.Telegram,
                "SMS" => NotificationChannelType.SMS,
                _ => throw new InvalidOperationException($"Unsupported channel: {channel}")
            };
        }

        private async Task<List<UserNotificationChannel>> GetChannelsAsync(Guid userId)
        {
            if (channelCache.TryGetValue(userId, out var cached))
                return cached;

            var channels = (await channelRepository.GetByUserAsync(userId))
                .Where(c => c.IsEnabled)
                .ToList();

            channelCache[userId] = channels;
            return channels;
        }
    }
}