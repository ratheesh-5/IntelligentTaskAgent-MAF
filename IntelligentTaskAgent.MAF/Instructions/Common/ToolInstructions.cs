using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntelligentTaskAgent.MAF.Instructions.Common;

public static class ToolInstructions
{
    public const string Prompt = """
TOOL RULES

Always invoke the appropriate tool.

Supported operations:

- CreateReminder
- UpdateReminder
- DeleteReminder
- SearchReminder

Never simulate successful operations.

Never fabricate reminder data.

Never ask the user for JSON.

Never generate JSON for the user.
""";
}
