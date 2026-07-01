using IntelligentTaskAgent.Application.Interfaces;
using IntelligentTaskAgent.Application.Services;
using IntelligentTaskAgent.Application.UserProfile.Interfaces;
using IntelligentTaskAgent.Application.UserProfile.Services;
using IntelligentTaskAgent.Core.Extensions;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using taskService = IntelligentTaskAgent.Core.Services.TaskOrchestrationService;

namespace IntelligentTaskAgent.Application.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddAppServices(this IServiceCollection services)
        {
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IUserNotificationChannelService, UserNotificationChannelService>();
            services.AddScoped<ReminderProcessor>();
            services.AddScoped<NotificationDispatcher>();
            services.AddScoped<ITelegramService, TelegramService>();
            services.AddScoped<IChatOrchestrationService, ChatOrchestrationService>();
            services.AddScoped<ITaskOrchestrationService, TaskOrchestrationService>();
            services.AddScoped<taskService>();
            services.AddCoreServices();

            services.AddScoped<IReminderService, ReminderService>();
            services.AddScoped<IUserProfileService, UserProfileService>();

            return services;
        }
    }
}
