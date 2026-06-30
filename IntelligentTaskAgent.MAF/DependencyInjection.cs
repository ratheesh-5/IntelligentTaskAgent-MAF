using IntelligentTaskAgent.MAF.Agents;
using IntelligentTaskAgent.MAF.Memory;
using IntelligentTaskAgent.MAF.Plugins;
using IntelligentTaskAgent.MAF.Providers;
using IntelligentTaskAgent.MAF.Runtime;
using Microsoft.Extensions.AI;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace IntelligentTaskAgent.MAF
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddMAF(
        this IServiceCollection services,
        IConfiguration configuration)
        {
            services.Configure<LlmOptions>(
                configuration.GetSection(LlmOptions.SectionName));

            services.AddSingleton<IChatClientFactory, OpenAICompatibleChatClientFactory>();

            services.AddSingleton<IChatClient>(sp =>
                sp.GetRequiredService<IChatClientFactory>().Create());

            services.AddScoped<IReminderAgent, ReminderAgent>();

            services.AddScoped<IAgentRuntime, AgentRuntime>();

            services.AddScoped<IReminderPlugin, ReminderPlugin>();

            services.AddSingleton<IConversationMemory, InMemoryConversationStore>();

            return services;
        }
    }
}
