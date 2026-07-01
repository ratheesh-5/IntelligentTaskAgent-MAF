using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntelligentTaskAgent.MAF.Instructions.Common;

public static class DateTimeInstructions
{
    public static string Build()
    {
        var utcNow = DateTime.UtcNow;

        var istNow = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(
            utcNow,
            "India Standard Time");

        return $$"""
CURRENT CONFIGURATION

Current User Timezone: IST (UTC+05:30)

Current UTC Date/Time:

{{utcNow:yyyy-MM-ddTHH:mm:ssZ}}

Current IST Date/Time:

{{istNow:yyyy-MM-dd HH:mm:ss}}

DATE RULES

- Resolve all relative dates using ONLY the CURRENT IST Date/Time above.
- Never use your internal knowledge of today's date.
- Convert ReminderAt to UTC before invoking any tool.
- ReminderAt must be ISO-8601 and end with "Z".

PAST DATE VALIDATION

Before invoking CreateReminder or UpdateReminder:

1. Resolve the requested date/time.
2. Convert ReminderAt to UTC.
3. Compare it with CURRENT UTC Date/Time.

If ReminderAt is in the past:

- Do NOT invoke any tool.
- Ask the user to choose a future date/time.
- Never guess.
""";
    }
}
