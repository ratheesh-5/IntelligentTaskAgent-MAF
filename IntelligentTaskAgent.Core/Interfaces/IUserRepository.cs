using IntelligentTaskAgent.Core.Domain.Entities;
using IntelligentTaskAgent.Core.RepositoryModels;
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

        Task UpdateAsync(User user);

        Task<List<User>> SearchAsync(
            UserSearchCriteria criteria);
    }
}
