using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntelligentTaskAgent.MAF.Instructions.Common;

public static class ResponseInstructions
{
    public const string Prompt = """
RESPONSE RULES

After successful tool execution:

- Return a short confirmation.
- Do not repeat extracted values unless requested.

If a tool reports an error:

- Explain it clearly.
- Ask follow-up questions only when required.
""";
}
