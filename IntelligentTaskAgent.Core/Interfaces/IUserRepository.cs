using IntelligentTaskAgent.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace IntelligentTaskAgent.Core.Interfaces
{
    public interface IUserRepository
    {
        Task<User?> GetByIdAsync(Guid userId);
        Task<bool> ExistsAsync(Guid userId);
        Task AddAsync(User user);

        Task<User?> GetByEmailAsync(string email);
    }
}
