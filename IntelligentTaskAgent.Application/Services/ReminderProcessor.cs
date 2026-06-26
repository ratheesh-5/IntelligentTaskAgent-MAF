using IntelligentTaskAgent.Core.Interfaces;
using IntelligentTaskAgent.Notifications.Common;
using IntelligentTaskAgent.Notifications.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace IntelligentTaskAgent.Application.Services
{
    public class ReminderProcessor
    {
        private readonly IReminderRepository reminderRepository;
        private readonly IUserNotificationChannelRepository channelRepository;
        private readonly NotificationDispatcher dispatcher;

        public ReminderProcessor(
            IReminderRepository reminderRepository,
            IUserNotificationChannelRepository channelRepository,
            NotificationDispatcher dispatcher)
        {
            this.reminderRepository = reminderRepository;
            this.channelRepository = channelRepository;
            this.dispatcher = dispatcher;
        }
        public async Task ProcessDueRemindersAsync()
        {
            var dueReminders = await reminderRepository.GetPendingAsync(DateTime.UtcNow);

            // TODO Update record in loop to database is not good need to change later
            foreach (var reminder in dueReminders)
            {
                // Use the userId-based dispatch with fallback
                await dispatcher.DispatchWithFallbackAsync(
                    reminder.UserId,
                    reminder.Message,
                    requiredChannel:reminder.Channel
                );

                reminder.MarkAsSent();
                await reminderRepository.UpdateAsync(reminder);
            }
        }
    }
}
