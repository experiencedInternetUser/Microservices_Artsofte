using System;

namespace User.Core.DTOs
{
    public record ProfileDto(Guid Id, Guid UserId, string? FullName, string? Bio, string? AvatarUrl);
    public record CreateProfileRequest(Guid UserId, string? FullName, string? Bio, string? AvatarUrl);
    public record UpdateProfileRequest(string? FullName, string? Bio, string? AvatarUrl);
}
