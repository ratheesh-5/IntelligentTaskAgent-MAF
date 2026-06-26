using System.Collections.Generic;

namespace IntelligentTaskAgent.MAF.Memory
{
    public class MemoryStore
    {
        private readonly IDictionary<string, object> _memories = new Dictionary<string, object>();

        public void Register(string key, object memory) => _memories[key] = memory;

        public object? Get(string key)
        {
            _memories.TryGetValue(key, out var m);
            return m;
        }

        public T? Get<T>(string key) where T : class
        {
            if (_memories.TryGetValue(key, out var memory))
            {
                return memory as T;
            }
            return null;
        }
    }
}
