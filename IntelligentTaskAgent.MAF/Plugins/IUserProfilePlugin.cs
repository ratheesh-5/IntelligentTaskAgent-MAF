using IntelligentTaskAgent.Application.UserProfile.Models;

namespace IntelligentTaskAgent.MAF.Plugins;

public interface IUserProfilePlugin
{
    Task<UserProfileDto?> GetUserByEmailAsync(
        string email);

    Task<List<UserSummary>> SearchUserAsync(
        string keyword);

    Task<UserProfileDto> CreateUserAsync(
        string name,
        string email);

    Task<UserProfileDto> UpdateUserProfileAsync(
        Guid userId,
        string? name,
        string? timeZone,
        string? language);
}