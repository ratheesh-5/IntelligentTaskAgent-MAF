using IntelligentTaskAgent.Core.Interfaces;
using Microsoft.SemanticKernel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace IntelligentTaskAgent.Core.AI.Plugins
{
    public class TaskPlugin
    {
        private readonly ILLMClient llmClient;

        public TaskPlugin(ILLMClient llmClient)
        {
            this.llmClient = llmClient;
        }
        [KernelFunction("ExtractTask")]
        public async Task<string> ExtractTask(string input)
        {
            var prompt = $$"""
You are an AI assistant.

Extract task reminder information and return STRICT JSON.

RULES:
- Return JSON ONLY
- No explanation
- Use ISO 8601 datetime (yyyy-MM-ddTHH:mm:ssZ)
- If no reminder, set reminderAt as null

Input:
"{{input}}"

Return JSON only:
{
  "Intent": "CreateTask",
  "TaskTitle": "",
  "Description": "",
  "ReminderAt": null
}
""";

            return await llmClient.CompleteAsync(prompt);
        }

    }
}
