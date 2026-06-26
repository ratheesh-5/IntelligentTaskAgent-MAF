using IntelligentTaskAgent.Core.Domain.Entities;
using IntelligentTaskAgent.Core.Interfaces;
using IntelligentTaskAgent.Repositories.DbContexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.SemanticKernel;

public class UserNotificationChannelRepository : IUserNotificationChannelRepository
{
    private readonly IntelligentTaskAgentDbContext db;

    public UserNotificationChannelRepository(IntelligentTaskAgentDbContext db)
    {
        this.db = db;
    }

    // Add new channel
    public async Task AddAsync(UserNotificationChannel channel)
    {
        db.UserNotificationChannels.Add(channel);
        await db.SaveChangesAsync();
    }

    // Update existing channel
    public async Task UpdateAsync(UserNotificationChannel channel)
    {
        db.UserNotificationChannels.Update(channel);
        await db.SaveChangesAsync();
    }

    // Get all channels for a user (enabled + disabled)
    public async Task<IEnumerable<UserNotificationChannel>> GetByUserAsync(Guid userId)
    {
        return await db.UserNotificationChannels
            .Where(x => x.UserId == userId)
            .ToListAsync();
    }

    // Get primary channel (enabled only)
    public async Task<UserNotificationChannel?> GetPrimaryAsync(Guid userId)
    {
        return await db.UserNotificationChannels
            .Where(x => x.UserId == userId && x.IsEnabled)
            .OrderByDescending(x => x.IsPrimary)
            .FirstOrDefaultAsync();
    }

    // Get enabled channels (used for fallback)
    public async Task<IEnumerable<UserNotificationChannel>> GetEnabledAsync(Guid userId)
    {
        return await db.UserNotificationChannels
            .Where(x => x.UserId == userId && x.IsEnabled)
            .ToListAsync();
    }

    public async Task<UserNotificationChannel?> GetByChannelAsync(string channel, string channelValue)
    {
        var result =  await db.UserNotificationChannels
            .AsNoTracking()
            .FirstOrDefaultAsync(x =>
                x.Channel == channel &&
                x.ChannelValue == channelValue &&
                x.IsEnabled);
        return result;
    }
}