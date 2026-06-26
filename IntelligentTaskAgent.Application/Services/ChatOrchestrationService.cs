using IntelligentTaskAgent.Application.Interfaces;
using IntelligentTaskAgent.Core.Context;
using IntelligentTaskAgent.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntelligentTaskAgent.Application.Services
{
    public sealed class ChatOrchestrationService : IChatOrchestrationService
    {
        private readonly AgentOrchestrator orchestrator;
        private readonly ITelegramService telegramService;
        private readonly IUserService userService;
        private readonly ITaskOrchestrationService taskOrchestrationService;
        public ChatOrchestrationService(
            AgentOrchestrator orchestrator,
            ITelegramService telegramService,
            IUserService userService,
            ITaskOrchestrationService taskOrchestrationService)
        {
            this.orchestrator = orchestrator;
            this.telegramService = telegramService;
            this.userService = userService;
            this.taskOrchestrationService = taskOrchestrationService;
        }

        public async Task<string> HandleMessageAsync(
    AgentContext context,
    CancellationToken ct)
        {
            var userId =
                await userService.FindOrCreateByTelegramAsync(context.ChatId, ct);

            context.UserId = userId;

            var state =
                await telegramService.GetConversationStateAsync(context.ChatId, ct)
                ?? new ConversationState();

            var response =
                await orchestrator.HandleAsync(context, state);

            await telegramService.SaveConversationStateAsync(
                context.ChatId,
                state,
                ct);

            if (state.Stage == ConversationStage.Completed &&
                            state.ExtractedTaskIntent != null)
            {
                // 🔹 5. Persist task (ONLY here)
                if (state.ExtractedTaskIntent != null)
                {
                    await taskOrchestrationService.HandleAsync(
                        context.Message, state.ExtractedTaskIntent);
                }
                // Clear memory after success
                await telegramService.DeleteConversationStateAsync(context.ChatId, ct);
            }
            return response.Message;
        }
    }

}
