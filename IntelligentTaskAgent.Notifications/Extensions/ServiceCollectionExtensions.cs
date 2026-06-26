using IntelligentTaskAgent.Notifications.Domain.Interfaces;
using IntelligentTaskAgent.Notifications.Infrastructure.Email;
using IntelligentTaskAgent.Notifications.Infrastructure.Sms;
using IntelligentTaskAgent.Notifications.Infrastructure.Telegram;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace IntelligentTaskAgent.Notifications.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddNotifications(this IServiceCollection services
            , IConfiguration configuration)
        {
            services.AddEmailServices(configuration);
            services.AddTelegramServices(configuration);
            services.AddSmsServices();
            return services;
        }
        public static void AddEmailServices(this IServiceCollection services, IConfiguration configuration)
        {
            var sectionEmail = configuration.GetSection(EmailOptions.SectionName);
            services.Configure<EmailOptions>(options => sectionEmail.Bind(options));
            services.AddScoped<INotificationSender, SmtpEmailSender>();
        }
        public static void AddTelegramServices(this IServiceCollection services, IConfiguration configuration)
        {
            var sectionTelegram = configuration.GetSection(TelegramOptions.SectionName);
            services.Configure<TelegramOptions>(options => sectionTelegram.Bind(options));
            services.AddScoped<INotificationSender, TelegramSender>();
            services.AddHttpClient<TelegramSender>();
        }
        public static void AddSmsServices(this IServiceCollection services)
        {
            services.AddScoped<INotificationSender, MockSmsSender>();
        }
    }
}
