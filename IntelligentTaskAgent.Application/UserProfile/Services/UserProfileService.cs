using IntelligentTaskAgent.Application.UserProfile.Commands;
using IntelligentTaskAgent.Application.UserProfile.Interfaces;
using IntelligentTaskAgent.Application.UserProfile.Models;
using IntelligentTaskAgent.Core.Domain.Entities;
using IntelligentTaskAgent.Core.Interfaces;
using IntelligentTaskAgent.Core.RepositoryModels;

namespace IntelligentTaskAgent.Application.UserProfile.Services;

public sealed class UserProfileService : IUserProfileService
{
    private readonly IUserRepository repository;

    public UserProfileService(
        IUserRepository repository)
    {
        this.repository = repository;
    }

    public async Task<UserProfileDto?> GetByEmailAsync(
        GetUserProfileCommand command)
    {
        var user =
            await repository.GetByEmailAsync(command.Email);

        if (user == null)
        {
            return null;
        }

        return Map(user);
    }

    public async Task<UserProfileDto> CreateAsync(
        CreateUserCommand command)
    {
        var existing =
            await repository.GetByEmailAsync(command.Email);

        if (existing != null)
        {
            throw new InvalidOperationException(
                $"User '{command.Email}' already exists.");
        }

        var user = new User
        {
            UserId = Guid.NewGuid(),
            Name = command.Name,
            Email = command.Email,
            CreatedAt = DateTime.UtcNow,
            IsActive = true
        };

        await repository.AddAsync(user);

        return Map(user);
    }

    public async Task<UserProfileDto> UpdateAsync(
        UpdateUserProfileCommand command)
    {
        var user =
            await repository.GetByIdAsync(command.UserId);

        if (user == null)
        {
            throw new InvalidOperationException(
                "User not found.");
        }

        if (!string.IsNullOrWhiteSpace(command.Name))
        {
            user.Name = command.Name;
        }

        if (!string.IsNullOrWhiteSpace(command.TimeZone))
        {
            user.TimeZone = command.TimeZone;
        }

        if (!string.IsNullOrWhiteSpace(command.Language))
        {
            user.Language = command.Language;
        }

        user.UpdatedAt = DateTime.UtcNow;

        await repository.UpdateAsync(user);

        return Map(user);
    }

    private static UserProfileDto Map(
        User user)
    {
        return new UserProfileDto
        {
            UserId = user.UserId,
            Name = user.Name,
            Email = user.Email,
            TimeZone = user.TimeZone,
            Language = user.Language,
            IsActive = user.IsActive
        };
    }

    public async Task<List<UserSummary>> SearchAsync(
    SearchUserCommand command)
    {
        var criteria = new UserSearchCriteria
        {
            Keyword = command.Keyword,
            OnlyActive = command.OnlyActive,
            Top = command.Top
        };

        var users = await repository.SearchAsync(criteria);

        return users
            .Select(x => new UserSummary
            {
                UserId = x.UserId,
                Name = x.Name,
                Email = x.Email
            })
            .ToList();
    }
}
