using IntelligentTaskAgent.Application.Interfaces;
using IntelligentTaskAgent.Application.Services;
using IntelligentTaskAgent.Core.Context;
using IntelligentTaskAgent.Core.Domain.Entities;
using IntelligentTaskAgent.Core.Responses;
using IntelligentTaskAgent.Telegram.Models;

namespace IntelligentTaskAgent.Telegram.Handlers;

public sealed class TelegramConversationHandler : ITelegramConversationHandler
{
    private readonly ITelegramBotClient bot;
    private readonly ITelegramService telegramService;
    private readonly IUserService userService;
    private readonly IChatOrchestrationService chatOrchestrationService;

    public TelegramConversationHandler(
        ITelegramBotClient bot,
        ITelegramService telegramService,
        IUserService userService,
        IChatOrchestrationService chatOrchestrationService)
    {
        this.bot = bot;
        this.telegramService = telegramService;
        this.userService = userService;
        this.chatOrchestrationService = chatOrchestrationService;
    }

    /* Webhooks tested code from telegram chat bot
    public async Task HandleAsync(TelegramUpdate update, CancellationToken ct = default)
    {
        try
        {
            var msg = update.Message;
            if (msg == null) return;

            var chatId = msg.Chat.Id;
            var text = (msg.Text ?? string.Empty).Trim();

            // 1) /start -> ask email
            if (string.Equals(text, "/start", StringComparison.OrdinalIgnoreCase))
            {
                await telegramService.UpsertAsync(new TelegramOnboardingSession
                {
                    ChatId = chatId,
                    State = "WaitingForEmail"
                });

                await bot.SendMessageAsync(chatId,
                    "Welcome to IntelligentTaskAgent 👋\n\nPlease reply with your email to link your account.",
                    ct);

                return;
            }

            // 2) if session exists and waiting email -> link
            var session = await telegramService.GetAsync(chatId);

            if (session != null && session.State == "WaitingForEmail")
            {
                if (!EmailRegex.IsMatch(text))
                {
                    await bot.SendMessageAsync(chatId,
                        "That doesn’t look like a valid email. Please send a valid email (example: name@gmail.com).",
                        ct);
                    return;
                }

                // Find or create user by email
                var userId = await userService.FindOrCreateByEmailAsync(text);

                // Save Telegram channel
                await channelService.AddOrUpdateChannelAsync(
                    userId: userId,
                    channel: "Telegram",
                    channelValue: chatId.ToString(),
                    isPrimary: true);

                // Cleanup onboarding session
                await telegramService.DeleteAsync(chatId);

                await bot.SendMessageAsync(chatId,
                    "✅Linked successfully! You can now send tasks like:\n\"Remind me to buy milk tomorrow 9am\"",
                    ct);

                return;
            }

            // 3) Normal messages (later: pass to TaskAgent)
            await bot.SendMessageAsync(chatId,
                "I’m linked already. Send me a reminder like:\n\"Remind me to call John tomorrow at 10am\"",
                ct);
        }
        catch (Exception ex)
        {

            throw;
        }

    }*/

    public async Task HandleAsync(
    TelegramUpdate update,
    CancellationToken ct = default)
    {
        var msg = update.Message;
        if (msg == null || string.IsNullOrWhiteSpace(msg.Text))
            return;

        var chatId = msg.Chat.Id;
        var text = msg.Text.Trim();

        // 🔹 Build agent context (transport → application)
        var context = new AgentContext
        {
            ChatId = chatId,
            Message = text
        };

        // 🔹 Delegate EVERYTHING to Application layer
        var responseMessage =
            await chatOrchestrationService.HandleMessageAsync(context, ct);

        // 🔹 Reply to Telegram
        if (!string.IsNullOrWhiteSpace(responseMessage))
        {
            await bot.SendMessageAsync(chatId, responseMessage, ct);
        }
    }
}
