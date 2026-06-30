using IntelligentTaskAgent.MAF.AgentFactory;
using IntelligentTaskAgent.MAF.Agents;
using IntelligentTaskAgent.MAF.Memory;
using IntelligentTaskAgent.MAF.Plugins;
using IntelligentTaskAgent.MAF.Providers;
using IntelligentTaskAgent.MAF.Routing;
using IntelligentTaskAgent.MAF.Runtime;
using Microsoft.Extensions.AI;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using agentFactory = IntelligentTaskAgent.MAF.AgentFactory.AgentFactory;
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

            // Infrastructure
            services.AddSingleton<IChatClientFactory, OpenAICompatibleChatClientFactory>();

            services.AddTransient<IChatClient>(sp =>
                sp.GetRequiredService<IChatClientFactory>().Create());

            //Memory
            services.AddSingleton<IConversationMemory, InMemoryConversationStore>();

            // Agents
            services.AddScoped<IReminderAgent, ReminderAgent>();
            services.AddScoped<IRouterAgent, AgentRouter>();

            // Factory
            services.AddScoped<IAgentFactory, agentFactory>();

            // Runtime
            services.AddScoped<IAgentRuntime, AgentRuntime>();

            // Plugins
            services.AddScoped<IReminderPlugin, ReminderPlugin>();

            return services;
        }
    }
}
