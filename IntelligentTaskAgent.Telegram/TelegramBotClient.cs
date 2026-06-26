using System.Net.Http.Json;
using Microsoft.Extensions.Options;

namespace IntelligentTaskAgent.Telegram;

public sealed class TelegramOptions
{
    public const string SectionName = "Telegram";

    public string BotToken { get; set; } = string.Empty;
    public string WebhookSecret { get; set; } = string.Empty;
}

public interface ITelegramBotClient
{
    Task SendMessageAsync(long chatId, string text, CancellationToken ct = default);
}

public sealed class TelegramBotClient : ITelegramBotClient
{
    private readonly HttpClient http;
    private readonly TelegramOptions options;

    public TelegramBotClient(HttpClient http, IOptions<TelegramOptions> options)
    {
        this.http = http;
        this.options = options.Value;
    }

    public async Task SendMessageAsync(long chatId, string text, CancellationToken ct = default)
    {
        // Telegram endpoint: https://api.telegram.org/bot{token}/sendMessage
        var url = $"https://api.telegram.org/bot{options.BotToken}/sendMessage";

        var payload = new
        {
            chat_id = chatId,
            text = text
        };

        var resp = await http.PostAsJsonAsync(url, payload, ct);
        resp.EnsureSuccessStatusCode();
    }
}
