using IntelligentTaskAgent.Core.Domain;
using IntelligentTaskAgent.Core.Domain.Entities;
using IntelligentTaskAgent.Core.Interfaces;
using IntelligentTaskAgent.Core.RepositoryModels;
using IntelligentTaskAgent.Repositories.DbContexts;
using Microsoft.EntityFrameworkCore;

namespace IntelligentTaskAgent.Repositories.Services
{
    public class UserRepository : IUserRepository
    {
        private readonly IntelligentTaskAgentDbContext intelligentTaskAgentDbContext;
        public UserRepository(IntelligentTaskAgentDbContext intelligentTaskAgentDbContext)
        {
            this.intelligentTaskAgentDbContext = intelligentTaskAgentDbContext;
        }
        public async Task<User?> GetByIdAsync(Guid userId)
        {
            return await intelligentTaskAgentDbContext.Users
                .FirstOrDefaultAsync(x => x.UserId == userId);
        }
        public async Task<bool> ExistsAsync(Guid userId)
        {
            return await intelligentTaskAgentDbContext.Users.AnyAsync(x => x.UserId == userId);
        }
        public async Task AddAsync(User user)
        {
            await intelligentTaskAgentDbContext.Users.AddAsync(user);
            await intelligentTaskAgentDbContext.SaveChangesAsync();
        }

        public async Task<User?> GetByEmailAsync(string email)
        {
            return await intelligentTaskAgentDbContext.Users
                        .AsNoTracking()
                        .FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task UpdateAsync(User user)
        {
            this.intelligentTaskAgentDbContext.Users.Update(user);
            await this.intelligentTaskAgentDbContext.SaveChangesAsync();
        }

        public async Task<List<User>> SearchAsync(
            UserSearchCriteria criteria)
        {
            IQueryable<User> query =
                this.intelligentTaskAgentDbContext.Users;

            if (criteria.OnlyActive)
            {
                query = query.Where(x => x.IsActive);
            }

            if (!string.IsNullOrWhiteSpace(criteria.Keyword))
            {
                var keyword = criteria.Keyword.Trim();

                query = query.Where(x =>
                    (!string.IsNullOrWhiteSpace(x.Name) &&
                     EF.Functions.Like(x.Name, $"%{keyword}%"))
                    ||
                    EF.Functions.Like(x.Email, $"%{keyword}%"));
            }

            query = query.OrderBy(x => x.Name);

            if (criteria.Top.HasValue)
            {
                query = query.Take(criteria.Top.Value);
            }

            return await query.ToListAsync();
        }
    }
}
