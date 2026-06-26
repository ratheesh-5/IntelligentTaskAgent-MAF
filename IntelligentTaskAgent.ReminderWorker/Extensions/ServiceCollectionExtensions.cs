using IntelligentTaskAgent.Application.Extensions;
using IntelligentTaskAgent.Infrastructure.Extensions;
using IntelligentTaskAgent.Notifications.Extensions;
using IntelligentTaskAgent.Repositories.Extensions;
using IntelligentTaskAgent.Telegram.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntelligentTaskAgent.ReminderWorker.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddFunctionAppServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAppServices();
            services.AddNotifications(configuration);
            services.AddRepositoryServices(configuration);
            services.AddTelegram(configuration);
            services.AddInfrastructure(configuration);
            return services;
        }
    }
}
