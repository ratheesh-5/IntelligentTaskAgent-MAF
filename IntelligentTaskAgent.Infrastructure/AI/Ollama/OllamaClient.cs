using IntelligentTaskAgent.Core.Interfaces;
using Microsoft.Extensions.Options;
using System.Net.Http;
using System.Text;
using System.Text.Json;

namespace IntelligentTaskAgent.Infrastructure.AI.Ollama
{
    public class OllamaClient : ILLMClient
    {
        private readonly HttpClient httpClient;
        private readonly OllamaOptions ollamaOptions;

        // ✅ Expose model for logging / diagnostics
        public string ModelName => ollamaOptions.Model;

        public OllamaClient(
            HttpClient httpClient,
            IOptions<OllamaOptions> options)
        {
            this.httpClient = httpClient;
            this.ollamaOptions = options.Value;

            // this.httpClient.BaseAddress = new Uri(this.ollamaOptions.BaseUrl);
        }

        public async Task<string> CompleteAsync(string prompt)
        {
            var request = new
            {
                model = this.ollamaOptions.Model,
                prompt = prompt,
                stream = false
            };

            var json = JsonSerializer.Serialize(request);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await httpClient.PostAsync("/api/generate", content);
            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadAsStringAsync();
            return result;
        }
    }
}