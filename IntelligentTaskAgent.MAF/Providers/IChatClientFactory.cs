using Microsoft.Extensions.AI;

namespace IntelligentTaskAgent.MAF.Providers
{
    public interface IChatClientFactory
    {
        IChatClient Create();
    }
}
