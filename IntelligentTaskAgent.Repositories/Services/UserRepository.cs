using IntelligentTaskAgent.Core.Domain.Entities;
using IntelligentTaskAgent.Core.Interfaces;
using IntelligentTaskAgent.Repositories.DbContexts;
using Microsoft.EntityFrameworkCore;

namespace IntelligentTaskAgent.Repositories.Services
{
    public class UserRepository : IUserRepository
    {
        private readonly IntelligentTaskAgentDbContext db;
        public UserRepository(IntelligentTaskAgentDbContext db)
        {
            this.db = db;
        }
        public async Task<User?> GetByIdAsync(Guid userId)
        {
            return await db.Users
                .FirstOrDefaultAsync(x => x.UserId == userId);
        }
        public async Task<bool> ExistsAsync(Guid userId)
        {
            return await db.Users.AnyAsync(x => x.UserId == userId);
        }
        public async Task AddAsync(User user)
        {
            db.Users.Add(user);
            await db.SaveChangesAsync();
        }

        public async Task<User?> GetByEmailAsync(string email)
        {
            return await db.Users
                        .AsNoTracking()
                        .FirstOrDefaultAsync(u => u.Email == email);
        }
    }
}
