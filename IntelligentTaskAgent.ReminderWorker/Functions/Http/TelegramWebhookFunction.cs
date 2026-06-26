using IntelligentTaskAgent.Telegram;
using IntelligentTaskAgent.Telegram.Handlers;
using IntelligentTaskAgent.Telegram.Models;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace IntelligentTaskAgent.ReminderWorker.Functions.Http
{
    public sealed class TelegramWebhookFunction
    {
        private readonly ITelegramConversationHandler handler;
        private readonly TelegramOptions options;
        // Simple in-memory idempotency store
        // Key: Telegram update_id
        private static readonly ConcurrentDictionary<long, DateTime> ProcessedUpdates = new();

        public TelegramWebhookFunction(
            ITelegramConversationHandler handler,
            IOptions<TelegramOptions> options)
        {
            this.handler = handler;
            this.options = options.Value;
        }

        [Function("TelegramWebhook")]

        public async Task<HttpResponseData> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "telegram/webhook")] HttpRequestData req)
        {
            // 1️ Validate Telegram secret token
            if (!string.IsNullOrWhiteSpace(options.WebhookSecret))
            {
                if (!req.Headers.TryGetValues("X-Telegram-Bot-Api-Secret-Token", out var values) ||
                    values.FirstOrDefault() != options.WebhookSecret)
                {
                    return req.CreateResponse(HttpStatusCode.Forbidden);
                }
            }

            // 2 Read body
            var body = await new StreamReader(req.Body).ReadToEndAsync();
            if (string.IsNullOrWhiteSpace(body))
                return req.CreateResponse(HttpStatusCode.OK);

            TelegramUpdate? update;
            try
            {
                update = JsonSerializer.Deserialize<TelegramUpdate>(body);
            }
            catch
            {
                // Invalid payload → acknowledge Telegram
                return req.CreateResponse(HttpStatusCode.OK);
            }

            if (update?.UpdateId == null)
                return req.CreateResponse(HttpStatusCode.OK);

            // 3️ Idempotency check
            if (!ProcessedUpdates.TryAdd(update.UpdateId, DateTime.UtcNow))
            {
                // Duplicate delivery
                return req.CreateResponse(HttpStatusCode.OK);
            }

            // Optional cleanup (prevents memory leak)
            CleanupOldUpdates();

            // 4 IMPORTANT: Await handler (NO Task.Run)
            await handler.HandleAsync(update);

            // 5️ Always return 200 OK to Telegram
            return req.CreateResponse(HttpStatusCode.OK);
        }
        private static void CleanupOldUpdates()
        {
            var cutoff = DateTime.UtcNow.AddMinutes(-10);

            foreach (var item in ProcessedUpdates)
            {
                if (item.Value < cutoff)
                {
                    ProcessedUpdates.TryRemove(item.Key, out _);
                }
            }
        }
    }

}
