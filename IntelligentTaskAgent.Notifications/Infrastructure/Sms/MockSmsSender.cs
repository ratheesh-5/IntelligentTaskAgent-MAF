using IntelligentTaskAgent.Notifications.Domain.Enums;
using IntelligentTaskAgent.Notifications.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace IntelligentTaskAgent.Notifications.Infrastructure.Sms
{
    public class MockSmsSender : INotificationSender
    {
        public NotificationChannelType ChannelType => NotificationChannelType.SMS;

        public Task SendAsync(string destination, string message, string? subject = null)
        {
            // Mock implementation
            Console.WriteLine($"[SMS MOCK] To: {destination}");
            Console.WriteLine($"Message: {message}");

            return Task.CompletedTask;
        }
    }
}
