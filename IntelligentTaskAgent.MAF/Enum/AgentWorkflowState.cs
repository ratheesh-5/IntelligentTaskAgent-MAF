using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntelligentTaskAgent.MAF.Enum
{
    public enum AgentWorkflowState
    {
        Unknown = 0,

        WaitingForUserIdentification = 1,

        UserIdentified = 2,

        ReminderInProgress = 3,

        Completed = 4
    }
}
