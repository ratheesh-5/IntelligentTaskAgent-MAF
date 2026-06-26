using System.Threading.Tasks;

namespace IntelligentTaskAgent.MAF.Memory
{
    public class ConversationMemory
    {
        public Task<T?> GetAsync<T>(string key)
        {
            return Task.FromResult<T?>(default);
        }

        public Task SaveAsync(string key, object value)
        {
            return Task.CompletedTask;
        }
    }
}
