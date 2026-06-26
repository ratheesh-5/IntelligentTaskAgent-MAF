using IntelligentTaskAgent.Notifications.Domain.Entities;
using IntelligentTaskAgent.Notifications.Domain.Enums;
using System.Threading.Tasks;

namespace IntelligentTaskAgent.Notifications.Domain.Interfaces
{
    public interface INotificationSender
    {
        NotificationChannelType ChannelType { get; }
        Task SendAsync(string destination, string message, string? subject = null);
    }
}
