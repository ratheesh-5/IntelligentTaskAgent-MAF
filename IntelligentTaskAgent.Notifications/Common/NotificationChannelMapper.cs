using IntelligentTaskAgent.Notifications.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace IntelligentTaskAgent.Notifications.Common
{
    public static class NotificationChannelMapper
    {
        public static NotificationChannelType ToNotificationChannelType(int channelId)
        {
            return channelId switch
            {
                0 => NotificationChannelType.Email,
                1 => NotificationChannelType.Telegram,
                2 => NotificationChannelType.SMS,
                3 => NotificationChannelType.WhatsApp,
                _ => throw new InvalidOperationException($"Unsupported channelId: {channelId}")
            };
        }
    }
}
