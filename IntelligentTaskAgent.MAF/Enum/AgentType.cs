using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace IntelligentTaskAgent.MAF.Enum
{

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum AgentType
    {
        Unknown = 0,

        Reminder = 1,

        UserProfile = 2,

        Notification = 3,

        Reporting = 4
    }
}
