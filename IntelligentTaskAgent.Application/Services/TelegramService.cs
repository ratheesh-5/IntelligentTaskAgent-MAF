using IntelligentTaskAgent.Application.Interfaces;
using IntelligentTaskAgent.Core.Context;
using IntelligentTaskAgent.Core.Domain.Entities;
using IntelligentTaskAgent.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace IntelligentTaskAgent.Application.Services
{
    public class TelegramService : ITelegramService
    {
        private readonly ITelegramOnboardingSessionRepository telegramOnboardingSessionRepository;
        private readonly ITelegramConversationStateRepository telegramConversationStateRepository;

        public TelegramService(
            ITelegramOnboardingSessionRepository telegramOnboardingSessionRepository
            , ITelegramConversationStateRepository telegramConversationStateRepository)
        {
               this.telegramOnboardingSessionRepository = telegramOnboardingSessionRepository;
            this.telegramConversationStateRepository = telegramConversationStateRepository;
        }
        public async Task DeleteAsync(long chatId)
        {
            await this.telegramOnboardingSessionRepository.DeleteAsync(chatId);
        }

        public async Task<TelegramOnboardingSession?> GetAsync(long chatId)
        {
            return await this.telegramOnboardingSessionRepository.GetAsync(chatId);
        }

        public async Task UpsertAsync(TelegramOnboardingSession session)
        {
            await this.telegramOnboardingSessionRepository.UpsertAsync(session);
        }

        public async Task<ConversationState?> GetConversationStateAsync(long chatId, CancellationToken ct = default)
        {
            var entity = await this.telegramConversationStateRepository.GetAsync(chatId, ct);
            if (entity == null)
                return null;

            return JsonSerializer.Deserialize<ConversationState>(entity.StateJson);
        }

        public async Task SaveConversationStateAsync(long chatId, ConversationState state, CancellationToken ct = default)
        {
            var json = JsonSerializer.Serialize(state);

            var entity = new TelegramConversationStateEntity
            {
                ChatId = chatId,
                StateJson = json,
                UpdatedAtUtc = DateTime.UtcNow
            };

            await this.telegramConversationStateRepository.UpsertAsync(entity, ct);
        }

        public async Task DeleteConversationStateAsync(long chatId, CancellationToken ct = default)
        {
            await this.telegramConversationStateRepository.DeleteAsync(chatId, ct);
        }

    }
}
