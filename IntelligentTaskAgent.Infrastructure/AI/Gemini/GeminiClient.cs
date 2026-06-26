using IntelligentTaskAgent.Core.Interfaces;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Net.Http.Json;
using System.Text;

namespace IntelligentTaskAgent.Infrastructure.AI.Gemini
{
    public class GeminiClient : ILLMClient
    {
        private readonly HttpClient httpClient;
        private readonly GeminiOptions options;

        public GeminiClient(
            HttpClient httpClient,
            IOptions<GeminiOptions> options)
        {
            this.httpClient = httpClient;
            this.options = options.Value;
        }

        public string ModelName => options.Model;

        public async Task<string> CompleteAsync(string prompt)
        {
            // 1. Ensure BaseUrl doesn't end with a slash to prevent double-slashes
            var baseUrl = options.BaseUrl.TrimEnd('/');

            // Use the v1 endpoint for the stable gemini-2.5-flash model
            var url = $"{baseUrl}/v1/models/{options.Model}:generateContent?key={options.ApiKey}";

            var request = new
            {
                contents = new[]
                {
            new { parts = new[] { new { text = prompt } } }
                }
            };

            var response = await httpClient.PostAsJsonAsync(url, request);

            if (!response.IsSuccessStatusCode)
            {
                var errorBody = await response.Content.ReadAsStringAsync();
                // This will help you see the EXACT reason Google is rejecting the call
                throw new HttpRequestException($"Gemini API Error ({response.StatusCode}): {errorBody}");
            }

            return await response.Content.ReadAsStringAsync();
        }
    }

}
