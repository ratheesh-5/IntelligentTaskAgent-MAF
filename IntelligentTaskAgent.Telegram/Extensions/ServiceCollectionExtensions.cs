using IntelligentTaskAgent.Application.Extensions;
using IntelligentTaskAgent.Telegram.Handlers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntelligentTaskAgent.Telegram.Extensions
{
    public static  class ServiceCollectionExtensions
    {
        public static IServiceCollection AddTelegram(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddHttpClient<ITelegramBotClient, TelegramBotClient>();
            services.AddScoped<ITelegramConversationHandler, TelegramConversationHandler>();

            var section = configuration.GetSection(TelegramOptions.SectionName);
            services.Configure<TelegramOptions>(options => section.Bind(options));

            services.AddAppServices();
            return services;
        }

    }
}
