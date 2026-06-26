using IntelligentTaskAgent.Notifications.Domain.Entities;
using IntelligentTaskAgent.Notifications.Domain.Enums;
using IntelligentTaskAgent.Notifications.Domain.Interfaces;
using Microsoft.Extensions.Options;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace IntelligentTaskAgent.Notifications.Infrastructure.Telegram
{
    public class TelegramSender : INotificationSender
    {
        private readonly HttpClient httpClient;
        private readonly TelegramOptions options;

        public TelegramSender(
            HttpClient httpClient,
            IOptions<TelegramOptions> options)
        {
            this.httpClient = httpClient;
            this.options = options.Value;
        }

        public NotificationChannelType ChannelType =>
            NotificationChannelType.Telegram;

        public async Task SendAsync(
            string destination,
            string message,
            string? subject = null)
        {
            if (string.IsNullOrWhiteSpace(destination))
                throw new ArgumentException("Telegram chatId is required", nameof(destination));

            if (string.IsNullOrWhiteSpace(message))
                throw new ArgumentException("Message cannot be empty", nameof(message));

            // Telegram API endpoint
            var url = $"{options.BaseUrl}/bot{options.BotToken}/sendMessage";

            var telegramMessage = $"⏰ Reminder:\n{message}";

            var payload = new
            {
                chat_id = destination,
                text = telegramMessage,
                parse_mode = "HTML"
            };

            var response = await httpClient.PostAsJsonAsync(url, payload);

            if (!response.IsSuccessStatusCode)
            {
                // Read the actual error body from Telegram
                var errorJson = await response.Content.ReadAsStringAsync();

                if (response.StatusCode == System.Net.HttpStatusCode.Forbidden)
                {
                    // Typical body: {"ok":false,"error_code":403,"description":"Forbidden: bot was blocked by the user"}
                    throw new InvalidOperationException($"Access Denied by Telegram: {errorJson}");
                }

                throw new InvalidOperationException($"Telegram send failed ({response.StatusCode}): {errorJson}");
            }
        }
    }
}
