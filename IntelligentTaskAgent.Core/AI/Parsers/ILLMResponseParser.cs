using System;
using System.Collections.Generic;
using System.Text;

namespace IntelligentTaskAgent.Core.AI.Parsers
{
    public interface ILLMResponseParser<T>
    {
        T Parse(string rawResponse);
    }
}
