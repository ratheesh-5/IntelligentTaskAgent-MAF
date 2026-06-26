using IntelligentTaskAgent.Core.AI.Plugins;
using IntelligentTaskAgent.Core.Interfaces;
using Microsoft.SemanticKernel;
using System;
using System.Collections.Generic;
using System.Text;
using sematicKernel = Microsoft.SemanticKernel.Kernel;
namespace IntelligentTaskAgent.Core.AI.Kernel
{
    public class KernelFactory : IKernelFactory
    {
        private readonly TaskPlugin taskPlugin;
        public KernelFactory(
            TaskPlugin taskPlugin)
        {
            this.taskPlugin = taskPlugin;
        }
        public sematicKernel Create()
        {
            var builder = sematicKernel.CreateBuilder();

            // ❌ Do NOT add OpenAI connector here
            // ✔ Ollama is already abstracted via ILLMClient

            var kernel = builder.Build();

            // ✅ Register plugin WITH DI
            kernel.Plugins.AddFromObject(taskPlugin, "TaskPlugin");

            return kernel;
        }
    }
}
