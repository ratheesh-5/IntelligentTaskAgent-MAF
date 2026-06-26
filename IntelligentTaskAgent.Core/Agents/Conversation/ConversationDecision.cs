using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntelligentTaskAgent.Core.Agents.Conversation
{
    public enum ConversationDecision
    {
        TaskIntent,
        UserProfile,
        Unsupported,
        OutOfScope
    }
}
