using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.SemanticKernel;
using semanticKernel = Microsoft.SemanticKernel.Kernel;
namespace IntelligentTaskAgent.Core.AI.Kernel
{
    public interface IKernelFactory
    {
        semanticKernel Create();
    }
}
