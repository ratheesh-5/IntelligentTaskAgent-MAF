using IntelligentTaskAgent.Core.Domain.Entities;
using System;
using System.Threading.Tasks;

namespace IntelligentTaskAgent.Application.Interfaces
{
    public interface IUserService
    {
        Task<Guid> CreateAsync(string? name);

        Task<User> CreateIfNotExistsAsync(Guid userId, string? name);

        Task<Guid> FindOrCreateByEmailAsync(string email);

        Task<Guid> FindOrCreateByTelegramAsync(long chatId, CancellationToken ct = default);
    }
}