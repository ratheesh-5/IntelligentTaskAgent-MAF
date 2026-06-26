using Microsoft.Extensions.AI;
using Microsoft.Extensions.Options;
using OpenAI;
using OpenAI.Chat;
using System;
using System.ClientModel;

namespace IntelligentTaskAgent.MAF.Providers
{
    public sealed class OpenAICompatibleChatClientFactory : IChatClientFactory
    {
        private readonly LlmOptions _options;

        public OpenAICompatibleChatClientFactory(IOptions<LlmOptions> options)
        {
            _options = options.Value;
        }

        public IChatClient Create()
        {
            var chatClient = new ChatClient(
                _options.Model,
                new ApiKeyCredential(_options.ApiKey),
                new OpenAIClientOptions
                {
                    Endpoint = new Uri(_options.BaseUrl)
                });

            return chatClient.AsIChatClient();
        }
    }
}
