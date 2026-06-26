using IntelligentTaskAgent.Core.Agents;
using IntelligentTaskAgent.Core.Agents.Conversation;
using IntelligentTaskAgent.Core.Agents.TaskIntent;
using IntelligentTaskAgent.Core.Agents.UserProfile;
using IntelligentTaskAgent.Core.AI.Kernel;
using IntelligentTaskAgent.Core.AI.Models;
using IntelligentTaskAgent.Core.AI.Parsers;
using IntelligentTaskAgent.Core.AI.Plugins;
using IntelligentTaskAgent.Core.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.SemanticKernel;
using System;
using System.Collections.Generic;
using System.Text;

namespace IntelligentTaskAgent.Core.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddCoreServices(this IServiceCollection services)
        {
            services.AddScoped<TaskAgent>();
            services.AddScoped<TaskOrchestrationService>();

            //services.AddScoped<IKernelFactory, KernelFactory>();

            //services.AddScoped<Kernel>(sp =>
            //{
            //    var factory = sp.GetRequiredService<IKernelFactory>();
            //    return factory.Create(); // use the actual factory method name
            //});

            //services.AddScoped<TaskPlugin>();

            services.AddScoped<AgentLoggingService>();
            services.AddAgentServices();
            //services.AddScoped<ILLMResponseParser<TaskIntent>, OllamaResponseParser>();

            services.AddScoped<ITaskService, TaskService>();

            return services;
        }

        public static void AddAgentServices(this IServiceCollection services)
        {
            services.AddScoped<ITaskIntentAgent, TaskIntentAgent>();
            services.AddScoped<IConversationAgent, ConversationAgent>();
            services.AddScoped<IUserProfileAgent, UserProfileAgent>();
            services.AddScoped<AgentOrchestrator>();
        }
    }
}
