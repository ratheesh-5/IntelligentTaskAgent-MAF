using IntelligentTaskAgent.Application.Extensions;
using IntelligentTaskAgent.Core.Extensions;
using IntelligentTaskAgent.Infrastructure.Extensions;
using IntelligentTaskAgent.MAF;
using IntelligentTaskAgent.Repositories.Extensions;
using Microsoft.Extensions.DependencyInjection;

namespace IntelligentTaskAgent.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection ConfigureServices(this IServiceCollection services, IConfiguration configuration)
        {
            // Register repository services here
            // services.AddScoped<IYourRepository, YourRepositoryImplementation>();
            services.AddAppServices();
            services.AddCoreServices();
            services.AddInfrastructure(configuration);
            services.AddRepositoryServices(configuration);
            services.AddMAF(configuration);
            return services;
        }
    }
}
