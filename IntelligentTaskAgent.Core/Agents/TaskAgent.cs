using IntelligentTaskAgent.Core.AI.Kernel;
using IntelligentTaskAgent.Core.AI.Models;
using IntelligentTaskAgent.Core.AI.Parsers;
using IntelligentTaskAgent.Core.AI.Prompts;
using IntelligentTaskAgent.Core.Domain;
using IntelligentTaskAgent.Core.Interfaces;
using IntelligentTaskAgent.Core.Services;
using IntelligentTaskAgent.Core.Utilities;
using Microsoft.SemanticKernel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace IntelligentTaskAgent.Core.Agents
{
    public class TaskAgent
    {
        // private readonly IKernelFactory kernelFactory;
        private readonly ILLMResponseParser<TaskIntentResult> responseParser;
        private readonly AgentLoggingService agentLoggingService;
        private readonly ILLMClient lLMClient;
        private readonly ITaskPromptProvider taskPromptProvider;

        public TaskAgent(
            // IKernelFactory kernelFactory
            ILLMResponseParser<TaskIntentResult> responseParser
            , AgentLoggingService agentLoggingService
            ,ILLMClient lLMClient
            , ITaskPromptProvider taskPromptProvider)
        {
            // this.kernelFactory = kernelFactory;
            this.responseParser = responseParser;
            this.agentLoggingService = agentLoggingService;
            this.lLMClient = lLMClient;
            this.taskPromptProvider = taskPromptProvider;
        }

        public async Task<(TaskIntentResult Intent, Guid CorrelationId)> AnalyzeAsync(string userInput)
        {
            var correlationId = Guid.NewGuid();
            // Kernel kernel = this.kernelFactory.Create();
            try
            {
                //var rawResponse = await kernel.InvokeAsync<string>(
                //    "TaskPlugin",
                //    "ExtractTask",
                //    new() { ["input"] = userInput }
                //);
                var prompt = await this.taskPromptProvider
                .BuildTaskExtractionPromptAsync(
                    userInput,
                    DateTime.UtcNow);
                var raw = await this.lLMClient.CompleteAsync(prompt); 
                var intent = this.responseParser.Parse(raw);

                await agentLoggingService.LogAsync(
                    correlationId: correlationId,
                    userInput: userInput,
                    agentAction: "ExtractTask",
                    modelUsed: this.lLMClient.ModelName,
                    output: raw,
                    success: true
                );

                return (intent, correlationId);
            }
            catch (Exception ex)
            {
                await agentLoggingService.LogAsync(
                    correlationId: correlationId,
                    userInput: userInput,
                    agentAction: "ExtractTask",
                    modelUsed: this.lLMClient.ModelName,
                    success: false,
                    errorMessage: ex.Message
                );

                return (new TaskIntentResult { Intent = "Unknown" }, correlationId);
            }
        }
    }
}
