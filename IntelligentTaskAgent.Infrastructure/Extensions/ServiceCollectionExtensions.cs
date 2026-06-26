using IntelligentTaskAgent.Core.AI.Models;
using IntelligentTaskAgent.Core.AI.Parsers;
using IntelligentTaskAgent.Core.AI.Prompts;
using IntelligentTaskAgent.Core.Interfaces;
using IntelligentTaskAgent.Infrastructure.AI.Enum;
using IntelligentTaskAgent.Infrastructure.AI.Gemini;
using IntelligentTaskAgent.Infrastructure.AI.Ollama;
using IntelligentTaskAgent.Infrastructure.AI.Prompts;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Http;
using Microsoft.Extensions.Options;
using System;
using System.Net.Http;

namespace IntelligentTaskAgent.Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddInfrastructure(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            // Read provider as raw string and parse into the enum to avoid needing ConfigurationBinder extensions.
            var providerString = configuration["LLM:Provider"];
            if (string.IsNullOrWhiteSpace(providerString) ||
                !Enum.TryParse<LLMProvider>(providerString, ignoreCase: true, out var provider))
            {
                throw new InvalidOperationException(
                    $"Missing or invalid configuration value for 'LLM:Provider'. Value: '{providerString ?? "<null>"}'");
            }

            services.AddScoped<ITaskPromptProvider, DefaultTaskPromptProvider>();
            services.AddScoped<IUserProfilePromptProvider, DefaultUserProfilePromptProvider>();
            services.AddScoped<IConversationPromptProvider, DefaultConversationPromptProvider>();

            switch (provider)
            {
                case LLMProvider.Ollama:
                    RegisterOllama(services, configuration);
                    break;

                case LLMProvider.Gemini:
                    RegisterGemini(services, configuration);
                    break;

                default:
                    throw new InvalidOperationException(
                        $"Unsupported LLM provider: {provider}");
            }

            return services;
        }

        // ---------------- PRIVATE REGISTRATIONS ----------------

        private static void RegisterOllama(
            IServiceCollection services,
            IConfiguration configuration)
        {
            // Use the IConfigurationSection and bind via the Action<TOptions> overload
            var section = configuration.GetSection(OllamaOptions.SectionName);
            services.Configure<OllamaOptions>(options => section.Bind(options));

            // Ensure OllamaClient can be resolved as the ILLMClient implementation.
            services.AddScoped<ILLMClient, OllamaClient>();

            services.AddScoped<ILLMResponseParser<TaskIntentResult>, OllamaTaskIntentParser>();

            services.Configure<OllamaOptions>(configuration.GetSection(OllamaOptions.SectionName));
            services.AddHttpClient<ILLMClient, OllamaClient>(client =>
            {
                var baseUrl = configuration[$"{OllamaOptions.SectionName}:BaseUrl"];
                if (string.IsNullOrWhiteSpace(baseUrl))
                {
                    throw new InvalidOperationException(
                        $"Missing or invalid configuration value for '{OllamaOptions.SectionName}:BaseUrl'.");
                }

                // Construct Uri after validating baseUrl is not null/whitespace to avoid CS8604.
                client.BaseAddress = new Uri(baseUrl);
            });
        }

        private static void RegisterGemini(
            IServiceCollection services,
            IConfiguration configuration)
        {
            // Use the IConfigurationSection and bind via the Action<TOptions> overload
            var section = configuration.GetSection(GeminiOptions.SectionName);
            services.Configure<GeminiOptions>(options => section.Bind(options));

            // Ensure GeminiClient can be resolved as the ILLMClient implementation.
            services.AddScoped<ILLMClient, GeminiClient>();

            //services.AddScoped<ILLMResponseParser<TaskIntentResult>, GeminiTaskIntentParser>();

            services.AddScoped(typeof(ILLMResponseParser<>), typeof(GeminiJsonResponseParser<>));
        }
    }
}
