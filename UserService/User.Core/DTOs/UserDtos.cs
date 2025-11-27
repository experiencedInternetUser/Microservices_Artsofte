using System;

namespace User.Core.DTOs
{
    public record UserDto(Guid Id, string Email, DateTime CreatedAt);
    public record CreateUserRequest(string Email, string Password);
    public record UpdateUserRequest(string? Email, string? Password);
}
