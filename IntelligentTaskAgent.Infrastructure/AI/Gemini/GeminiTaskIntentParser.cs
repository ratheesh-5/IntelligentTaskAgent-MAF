using IntelligentTaskAgent.Core.AI.Models;
using IntelligentTaskAgent.Core.AI.Parsers;
using System;
using System.Text.Json;

namespace IntelligentTaskAgent.Infrastructure.AI.Gemini
{
    /* commanded and replaced with GeminiJsonResponseParser
   public class GeminiTaskIntentParser
       : ILLMResponseParser<TaskIntentResult>
   {
       public TaskIntentResult Parse(string raw)
       {
           using var doc = JsonDocument.Parse(raw);

           // 1️ Navigate Gemini response
           var text = doc.RootElement
               .GetProperty("candidates")[0]
               .GetProperty("content")
               .GetProperty("parts")[0]
               .GetProperty("text")
               .GetString();

           if (string.IsNullOrWhiteSpace(text))
               throw new InvalidOperationException("Gemini response text is empty");

           // 2️  Clean markdown if present
           var cleanJson = CleanJson(text);

           // 3️ Deserialize final JSON
           return JsonSerializer.Deserialize<TaskIntentResult>(
               cleanJson,
               new JsonSerializerOptions
               {
                   PropertyNameCaseInsensitive = true
               })!;
       }

       /// <summary>
       /// Removes ```json / ``` wrappers if Gemini adds markdown
       /// </summary>
       private static string CleanJson(string text)
       {
           text = text.Trim();

           if (text.StartsWith("```"))
           {
               text = text
                   .Replace("```json", "", StringComparison.OrdinalIgnoreCase)
                   .Replace("```", "")
                   .Trim();
           }

           return text;
       }
   }*/
}