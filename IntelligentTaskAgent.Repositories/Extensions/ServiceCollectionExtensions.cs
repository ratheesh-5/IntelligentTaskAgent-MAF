using IntelligentTaskAgent.Core.Interfaces;
using IntelligentTaskAgent.Notifications.Domain.Interfaces;
using IntelligentTaskAgent.Repositories.Configurations;
using IntelligentTaskAgent.Repositories.DbContexts;
using IntelligentTaskAgent.Repositories.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Http; // Add this using directive
using System;
using System.Collections.Generic;
using System.Net.Http; // Add this using directive
using System.Text;

namespace IntelligentTaskAgent.Repositories.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddRepositoryServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<ITaskRepository, TaskRepository>();
            services.AddScoped<IReminderRepository, ReminderRepository>();
            services.AddScoped<IAgentExecutionLogRepository, AgentExecutionLogRepository>();

            // services.AddHttpClient<ILLMClient, OllamaClient>();

            services.AddDbContext<IntelligentTaskAgentDbContext>();

            services.AddHttpClient();

            services.AddScoped<IUserNotificationChannelRepository, UserNotificationChannelRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<INotificationLogRepository, NotificationLogRepository>();

            var section = configuration.GetSection(DatabaseOptions.SectionName);
            services.Configure<DatabaseOptions>(options => section.Bind(options));

            services.AddScoped<ITelegramOnboardingSessionRepository, TelegramOnboardingSessionRepository>();
            services.AddScoped<ITelegramConversationStateRepository, TelegramConversationStateRepository>();
            return services;
        }

    }
}
