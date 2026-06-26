using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntelligentTaskAgent.Core.Context
{
    public enum ConversationStage
    {
    None = 0,

    // Profile related
    AwaitingEmail = 10,
    AwaitingChannel = 11,

    // Task related
    AwaitingTaskConfirmation = 20,

    Completed = 100,
    }
}
