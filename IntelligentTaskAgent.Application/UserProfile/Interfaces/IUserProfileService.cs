using IntelligentTaskAgent.Application.UserProfile.Commands;
using IntelligentTaskAgent.Application.UserProfile.Models;

namespace IntelligentTaskAgent.Application.UserProfile.Interfaces;

public interface IUserProfileService
{
    Task<UserProfileDto?> GetByEmailAsync(
        GetUserProfileCommand command);

    Task<UserProfileDto> CreateAsync(
        CreateUserCommand command);

    Task<UserProfileDto> UpdateAsync(
        UpdateUserProfileCommand command);

    Task<List<UserSummary>> SearchAsync(
    SearchUserCommand command);
}
