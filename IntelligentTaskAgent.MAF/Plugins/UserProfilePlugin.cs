using IntelligentTaskAgent.Application.UserProfile.Commands;
using IntelligentTaskAgent.Application.UserProfile.Interfaces;
using IntelligentTaskAgent.Application.UserProfile.Models;

namespace IntelligentTaskAgent.MAF.Plugins;

public sealed class UserProfilePlugin : IUserProfilePlugin
{
    private readonly IUserProfileService userProfileService;

    public UserProfilePlugin(
        IUserProfileService userProfileService)
    {
        this.userProfileService = userProfileService;
    }

    public async Task<UserProfileDto?> GetUserByEmailAsync(
        string email)
    {
        return await userProfileService.GetByEmailAsync(
            new GetUserProfileCommand(email));
    }

    public async Task<List<UserSummary>> SearchUserAsync(
        string keyword)
    {
        return await userProfileService.SearchAsync(
            new SearchUserCommand(
                keyword,
                true,
                10));
    }

    public async Task<UserProfileDto> CreateUserAsync(
        string name,
        string email)
    {
        return await userProfileService.CreateAsync(
            new CreateUserCommand(
                name,
                email));
    }

    public async Task<UserProfileDto> UpdateUserProfileAsync(
        Guid userId,
        string? name,
        string? timeZone,
        string? language)
    {
        return await userProfileService.UpdateAsync(
            new UpdateUserProfileCommand(
                userId,
                name,
                timeZone,
                language));
    }
}