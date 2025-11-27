using System;

namespace User.Core.DTOs
{
    public record RoleDto(Guid Id, string Name, string? Description);
    public record CreateRoleRequest(string Name, string? Description);
    public record UpdateRoleRequest(string? Name, string? Description);
}
