using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntelligentTaskAgent.Core.Responses;

public class AgentResponse
{
    public string Message { get; private set; } = string.Empty;
    public bool Handled { get; private set; }

    private AgentResponse() { }

    public static AgentResponse Reply(string message)
        => new AgentResponse { Message = message, Handled = true };

    public static AgentResponse NotHandled()
        => new AgentResponse { Handled = false };
}

